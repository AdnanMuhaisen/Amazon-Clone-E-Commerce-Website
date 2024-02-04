using amazon_clone.Application.Interfaces;
using amazon_clone.Application.Managers;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.App_Details;
using amazon_clone.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IOrderProcessingService orderProcessingService;

        private readonly IShoppingCartSubTotalManager shoppingCartSubTotalManager;

        public IAppUnitOfWork _unitOfWork { get; }

        public ShoppingCartService(
            IAppUnitOfWork unitOfWork,
            IOrderProcessingService orderProcessingService,
            IShoppingCartSubTotalManager shoppingCartSubTotalManager
            )
        {
            _unitOfWork = unitOfWork;
            this.orderProcessingService = orderProcessingService;
            this.shoppingCartSubTotalManager = shoppingCartSubTotalManager;
        }


        public void AddProductToShoppingCart(int ProductID)
        {
            var targetOrderContainsTheCart = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts));

            if (targetOrderContainsTheCart is null)
            {
                // In this case, the customer is trying to add a product to the cart 
                //And the customer does not have a shopping cart created

                // we need to create a new order with the processing state
                orderProcessingService.CreateANewOrderWithShoppingCart();

                //now get the created order with shopping cart
                targetOrderContainsTheCart = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID
                && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts));
            }

            ArgumentNullException.ThrowIfNull((targetOrderContainsTheCart));

            var targetCustomerProductData = _unitOfWork
                .CustomerProductRepository
                .Get(x => x.ProductID == ProductID);

            ArgumentNullException.ThrowIfNull((targetCustomerProductData));

            if (targetOrderContainsTheCart!
                    .ShoppingCart
                    .CartProducts
                    .Any(x => x.CustomerProductID == targetCustomerProductData!.CustomerProductID))
            {
                //do something
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
                targetOrderContainsTheCart.ShoppingCart.PromoCodeID = PromoCodeService.GetShoppingCartPromoCode(targetOrderContainsTheCart.ShoppingCart.CartProducts.Count());


                targetOrderContainsTheCart.ShoppingCart.SubTotal += priceToAdd;
                targetOrderContainsTheCart.Total += priceToAdd;

                shoppingCartSubTotalManager.UpdateTheActualSubTotalOfAShoppingCart(targetOrderContainsTheCart.ShoppingCart);

                if (targetOrderContainsTheCart.ShoppingCart.PromoCodeID is not null)
                {
                    shoppingCartSubTotalManager.CalculateSubTotalAfterApplyingPromoCodeOnShoppingCart(targetOrderContainsTheCart.ShoppingCart);
                }

                _unitOfWork.Save();
            }
        }

        public void RemoveProductFromShoppingCart(int CustomerProductID)
        {
            var targetOrderContainsTheCart = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID
                && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts));

            ArgumentNullException.ThrowIfNull((targetOrderContainsTheCart));

            if (!(targetOrderContainsTheCart!
                .ShoppingCart
                .CartProducts
                .Any(x => x.CustomerProductID == CustomerProductID)))
            {
                throw new ShoppingCartException("can not remove a product that does not exist");
            }

            var targetProduct = targetOrderContainsTheCart
                .ShoppingCart
                .CartProducts
                .FirstOrDefault(x => x.CustomerProductID == CustomerProductID);

            ArgumentNullException.ThrowIfNull((targetProduct));

            decimal priceToSubtract = targetProduct!.Price - (targetProduct.Price * StaticDetails.PRODUCT_DICOUNT);

            //now delete the product from the cart
            var cartProduct = _unitOfWork
                .ShoppingCartProductRepository
                .Get(x => x.ShoppingCartID == targetOrderContainsTheCart.ShoppingCartID && x.CustomerProductID == CustomerProductID);

            ArgumentNullException.ThrowIfNull((cartProduct));

            targetOrderContainsTheCart.ShoppingCart.CartProducts.Remove(targetProduct);

            _unitOfWork
                .ShoppingCartProductRepository
                .Remove(cartProduct!);

            //update all the costs requirements:
            targetOrderContainsTheCart.ShoppingCart.PromoCodeID = PromoCodeService.GetShoppingCartPromoCode(targetOrderContainsTheCart.ShoppingCart.CartProducts.Count());


            targetOrderContainsTheCart.ShoppingCart.SubTotal -= priceToSubtract;
            targetOrderContainsTheCart.Total -= priceToSubtract;

            shoppingCartSubTotalManager.UpdateTheActualSubTotalOfAShoppingCart(targetOrderContainsTheCart.ShoppingCart);

            if (targetOrderContainsTheCart.ShoppingCart.PromoCodeID is not null)
            {
                shoppingCartSubTotalManager.CalculateSubTotalAfterApplyingPromoCodeOnShoppingCart(targetOrderContainsTheCart.ShoppingCart);
            }

            _unitOfWork.Save();
        }
    }
}
