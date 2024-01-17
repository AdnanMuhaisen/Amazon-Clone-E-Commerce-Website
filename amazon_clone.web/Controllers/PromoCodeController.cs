using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users;
using amazon_clone.Utility.App_Details;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.web.Controllers
{
    public class PromoCodeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PromoCodeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult ApplyShoppingCartPromoCode(string PromoCodeToApply)
        {
            if (string.IsNullOrWhiteSpace(PromoCodeToApply))
            {
                TempData["Invalid-PromoCode-Value"] = "Enter the correct promo code";
                return RedirectToAction("Index", "ShoppingCart");
            }

            var targetOrderContainsTheCart = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID && x.StatusID == (int)eOrderStatuses.PROCESSING,
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.ShoppingCartsProducts)
                .Include(x => x.ShoppingCart)
                .ThenInclude(y => y.CartPromoCode)!);

            ArgumentNullException.ThrowIfNull(nameof(targetOrderContainsTheCart));

            ArgumentNullException.ThrowIfNull(nameof(targetOrderContainsTheCart.ShoppingCart));
                        
            ArgumentNullException.ThrowIfNull(nameof(targetOrderContainsTheCart.ShoppingCart.CartPromoCode));

            if (targetOrderContainsTheCart?.ShoppingCart.CartPromoCode?.Code == PromoCodeToApply)
            {
                ApplyPromoCodeOnAShoppingCart(targetOrderContainsTheCart!.ShoppingCart);

                _unitOfWork.Save();
            }
            else
            {
                TempData["Invalid-PromoCode-Value"] = "Enter the correct promo code";
            }

            return RedirectToAction("Index","ShoppingCart");
        }

        [HttpPost]
        public IActionResult CancelAnAppOfShoppingCartPromoCode()
        {
            var targetOrderContainsTheCart = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID && x.StatusID == (int)eOrderStatuses.PROCESSING,
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.ShoppingCartsProducts));

            ArgumentNullException.ThrowIfNull(nameof(targetOrderContainsTheCart));

            ArgumentNullException.ThrowIfNull(nameof(targetOrderContainsTheCart.ShoppingCart));

            CancelAnAppOfPromoCode(targetOrderContainsTheCart!.ShoppingCart);

            _unitOfWork.Save();

            return RedirectToAction("Index","ShoppingCart");
        }

        private void CancelAnAppOfPromoCode(ShoppingCart shoppingCart)
        {
            ArgumentNullException.ThrowIfNull(nameof(shoppingCart));

            shoppingCart.ActualSubTotal = shoppingCart.SubTotal;
            shoppingCart.IsPromoCodeApplied = false;

            //update the total fees of the order
            UpdateTheTotalFeesOfTheCartOrder(shoppingCart.ActualSubTotal);
        }

        private void ApplyPromoCodeOnAShoppingCart(ShoppingCart shoppingCart)
        {
            ArgumentNullException.ThrowIfNull(nameof(shoppingCart));

            ArgumentNullException.ThrowIfNull(nameof(shoppingCart.PromoCodeID));

            shoppingCart.ActualSubTotal = (decimal)shoppingCart.SubTotalAfterApplyingPromoCode!;
            shoppingCart.IsPromoCodeApplied = true;

            //update the total fees of the order
            UpdateTheTotalFeesOfTheCartOrder(shoppingCart.ActualSubTotal);
        }

        private void UpdateTheTotalFeesOfTheCartOrder(decimal updatedSubTotal)
        {
            //update the total fees of the order
            var targetOrder = _unitOfWork
                .OrderRepository
                .Get(x => x.CustomerID == CurrentCustomer.UserID && x.StatusID == (int)eOrderStatuses.PROCESSING);

            ArgumentNullException.ThrowIfNull(nameof(targetOrder));

            targetOrder!.Total = updatedSubTotal + StaticDetails.ORDER_TAX + StaticDetails.ORDER_DELIVERY;
        }
    }
}
