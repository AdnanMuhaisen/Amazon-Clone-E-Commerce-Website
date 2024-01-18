using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users;
using amazon_clone.Models.View_Models;
using amazon_clone.Utility.App_Details;
using amazon_clone.Utility.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace amazon_clone.web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var customerOrder = _unitOfWork
                .OrderRepository
                .GetAsNoTracking(filter: x => x.CustomerID == CurrentCustomer.UserID
                && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts)
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartPromoCode!));

            if(customerOrder is null)
            {
                // we need to create a new order with the processing state
                ShoppingCart shoppingCart = new ShoppingCart
                {
                    PromoCodeID = null,
                    SubTotal = 0,
                    ActualSubTotal = 0
                };
                _unitOfWork.ShoppingCartRepository.Add(shoppingCart);
                _unitOfWork.Save();

                int targetShoppingCartID = _unitOfWork
                    .ShoppingCartRepository
                    .GetAllAsNoTracking()!
                    .Select(x => x.ShoppingCartID)
                    .OrderBy(x => x)
                    .LastOrDefault();

                ArgumentNullException.ThrowIfNull(nameof(targetShoppingCartID));

                customerOrder = new Order
                {
                    OrderDateTime = DateTime.Now,
                    CustomerID = CurrentCustomer.UserID!,
                    StatusID = (int)eOrderStatuses.PROCESSING,
                    ShoppingCartID = targetShoppingCartID
                };
                customerOrder.Tax = StaticDetails.ORDER_TAX;
                customerOrder.delivery = StaticDetails.ORDER_DELIVERY;
                customerOrder.Total = customerOrder.Tax + customerOrder.delivery;

                _unitOfWork.OrderRepository.Add(customerOrder);
                _unitOfWork.Save();
            }

            ArgumentNullException.ThrowIfNull(nameof(customerOrder));

            return View(new ShoppingCartViewModel(customerOrder, string.Empty));
        }

        public IActionResult RemoveProductFromShoppingCart(int CustomerProductID)
        {
            var targetOrderContainsTheCart = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts));

            ArgumentNullException.ThrowIfNull(nameof(targetOrderContainsTheCart));

            if(!(targetOrderContainsTheCart!
                .ShoppingCart
                .CartProducts
                .Any(x=>x.CustomerProductID == CustomerProductID)))
            {
                throw new ShoppingCartException("can not remove a product that does not exist");
            }

            var targetProduct = targetOrderContainsTheCart
                .ShoppingCart
                .CartProducts
                .FirstOrDefault(x => x.CustomerProductID == CustomerProductID);

            ArgumentNullException.ThrowIfNull(nameof(targetProduct));

            decimal priceToSubtract = targetProduct!.Price - (targetProduct.Price * StaticDetails.PRODUCT_DICOUNT);

            //now delete the product from the cart
            var cartProduct = _unitOfWork
                .ShoppingCartProductRepository
                .Get(x => x.ShoppingCartID == targetOrderContainsTheCart.ShoppingCartID && x.CustomerProductID == CustomerProductID);

            ArgumentNullException.ThrowIfNull(nameof(cartProduct));

            targetOrderContainsTheCart.ShoppingCart.CartProducts.Remove(targetProduct);

            _unitOfWork
                .ShoppingCartProductRepository
                .Remove(cartProduct!);

            //update all the costs requirements:
            targetOrderContainsTheCart.ShoppingCart.PromoCodeID = UpdateShoppingCartPromoCode(targetOrderContainsTheCart.ShoppingCart.CartProducts.Count());

            if(targetOrderContainsTheCart.ShoppingCart.PromoCodeID is not null)
            {
                CalculateSubTotalAfterApplyingPromoCodeOnShoppingCart(targetOrderContainsTheCart.ShoppingCart);
            }

            targetOrderContainsTheCart.ShoppingCart.SubTotal -= priceToSubtract;
            targetOrderContainsTheCart.Total -= priceToSubtract;

            UpdateTheActualSubTotalOfAShoppingCart(targetOrderContainsTheCart.ShoppingCart);

            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        public IActionResult AddProductToShoppingCart(int ProductID)
        {
            var targetOrderContainsTheCart = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts));

            ArgumentNullException.ThrowIfNull(nameof(targetOrderContainsTheCart));

            var targetCustomerProductData = _unitOfWork
                .CustomerProductRepository
                .Get(x => x.ProductID == ProductID);

            ArgumentNullException.ThrowIfNull(nameof(targetCustomerProductData));

            if (targetOrderContainsTheCart!
                    .ShoppingCart
                    .CartProducts
                    .Any(x => x.CustomerProductID == targetCustomerProductData!.CustomerProductID))
            {
                //do nothing
            }
            else
            {
                var cartProduct = new ShoppingCartProduct
                {
                    ShoppingCartID = targetOrderContainsTheCart.ShoppingCartID,
                    CustomerProductID = ProductID,
                    Quantity = 1
                };

                _unitOfWork.ShoppingCartProductRepository.Add(cartProduct);

                decimal priceToAdd = targetCustomerProductData!.Price - (targetCustomerProductData.Price * StaticDetails.PRODUCT_DICOUNT);
                targetOrderContainsTheCart.ShoppingCart.PromoCodeID = UpdateShoppingCartPromoCode(targetOrderContainsTheCart.ShoppingCart.CartProducts.Count());

                if (targetOrderContainsTheCart.ShoppingCart.PromoCodeID is not null)
                {
                    CalculateSubTotalAfterApplyingPromoCodeOnShoppingCart(targetOrderContainsTheCart.ShoppingCart);
                }

                targetOrderContainsTheCart.ShoppingCart.SubTotal += priceToAdd;
                targetOrderContainsTheCart.Total += priceToAdd;

                UpdateTheActualSubTotalOfAShoppingCart(targetOrderContainsTheCart.ShoppingCart);

                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// return the promo code id for the correct number of the shopping cart products
        /// </summary>
        /// <param name="numberOfShoppingCartProducts"></param>
        /// <returns></returns>
        private int? UpdateShoppingCartPromoCode(int numberOfShoppingCartProducts)
        {
            return numberOfShoppingCartProducts switch
            {
                1 => null,
                2 => (int)ePromoCodes.SHUBHO20,
                3 => (int)ePromoCodes.SHUBHO30,
                4 => (int)ePromoCodes.SHUBHO40,
                // in case of more than 4 products in the cart
                _ => (int)ePromoCodes.SHUBHO40
            };
        }

        private void CalculateSubTotalAfterApplyingPromoCodeOnShoppingCart(ShoppingCart shoppingCart)
        {
            ArgumentNullException.ThrowIfNull(nameof(shoppingCart));

            ArgumentNullException.ThrowIfNull(nameof(shoppingCart.CartProducts));

            ArgumentNullException.ThrowIfNull(nameof(shoppingCart.PromoCodeID));

            shoppingCart.SubTotalAfterApplyingPromoCode = shoppingCart.PromoCodeID switch
            {
                (int)ePromoCodes.SHUBHO20 => shoppingCart.SubTotal - (shoppingCart.SubTotal * 0.2m),
                (int)ePromoCodes.SHUBHO30 => shoppingCart.SubTotal - (shoppingCart.SubTotal * 0.3m),
                (int)ePromoCodes.SHUBHO40 => shoppingCart.SubTotal - (shoppingCart.SubTotal * 0.4m),
                _ => shoppingCart.SubTotal
            };
        }

         private void UpdateTheActualSubTotalOfAShoppingCart(ShoppingCart shoppingCart)
        {
            if (shoppingCart.IsPromoCodeApplied)
            {
                // because of the change on the promo code
                shoppingCart.ActualSubTotal = (decimal)shoppingCart.SubTotalAfterApplyingPromoCode!;
            }
            else
            {
                // because of the change on the Subtotal
                shoppingCart.ActualSubTotal = (decimal)shoppingCart.SubTotal!;
            }
        }
    }
}
