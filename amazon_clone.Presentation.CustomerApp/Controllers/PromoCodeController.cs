using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Presentation.CustomerApp.Filters;
using amazon_clone.Utility.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.web.Controllers
{
    public class PromoCodeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPromoCodeService promoCodeService;

        public PromoCodeController(IUnitOfWork unitOfWork, IPromoCodeService promoCodeService)
        {
            _unitOfWork = unitOfWork;
            this.promoCodeService = promoCodeService;
        }

        [HttpPost]
        public IActionResult ApplyShoppingCartPromoCode(string PromoCodeToApply)
        {
            try
            {
                promoCodeService.ApplyShoppingCartPromoCode(PromoCodeToApply);
            }
            catch(PromoCodeException)
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
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID 
                && (x.StatusID == (int)eOrderStatuses.PROCESSING|| x.StatusID == (int)eOrderStatuses.SHIPPED),
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.ShoppingCartsProducts));

            ArgumentNullException.ThrowIfNull((targetOrderContainsTheCart));

            ArgumentNullException.ThrowIfNull((targetOrderContainsTheCart.ShoppingCart));

            promoCodeService.CancelAnAppOfPromoCode(targetOrderContainsTheCart!.ShoppingCart);

            return RedirectToAction("Index","ShoppingCart");
        }
    }
}
