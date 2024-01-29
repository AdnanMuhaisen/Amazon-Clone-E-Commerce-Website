using amazon_clone.Application.Interfaces;
using amazon_clone.Application.Managers;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Application.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        public IUnitOfWork _unitOfWork { get; }
        public IShoppingCartCostsManager ShoppingCartCostsManager { get; }

        public PromoCodeService(IUnitOfWork unitOfWork, IShoppingCartCostsManager shoppingCartCostsManager)
        {
            _unitOfWork = unitOfWork;
            ShoppingCartCostsManager = shoppingCartCostsManager;
        }

        public void ApplyShoppingCartPromoCode(string PromoCodeToApply)
        {
            if (string.IsNullOrWhiteSpace(PromoCodeToApply))
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(PromoCodeToApply);
            }

            var targetOrderContainsTheCart = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID
                && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.ShoppingCartsProducts)
                .Include(x => x.ShoppingCart)
                .ThenInclude(y => y.CartPromoCode)!);

            ArgumentNullException.ThrowIfNull((targetOrderContainsTheCart));

            ArgumentNullException.ThrowIfNull((targetOrderContainsTheCart.ShoppingCart));

            ArgumentNullException.ThrowIfNull((targetOrderContainsTheCart.ShoppingCart.CartPromoCode));

            if (targetOrderContainsTheCart?.ShoppingCart.CartPromoCode?.Code == PromoCodeToApply)
            {
                ApplyPromoCodeOnAShoppingCart(targetOrderContainsTheCart!.ShoppingCart);

                _unitOfWork.Save();
            }
            else
            {
                throw new PromoCodeException("Invalid Shopping Cart Promo Code");
            }
        }

        public void ApplyPromoCodeOnAShoppingCart(ShoppingCart shoppingCart)
        {
            ArgumentNullException.ThrowIfNull((shoppingCart));

            ArgumentNullException.ThrowIfNull((shoppingCart.PromoCodeID));

            shoppingCart.ActualSubTotal = (decimal)shoppingCart.SubTotalAfterApplyingPromoCode!;
            shoppingCart.IsPromoCodeApplied = true;

            //update the total fees of the order
            ShoppingCartCostsManager.UpdateTheTotalFeesOfTheCartOrder(shoppingCart.ActualSubTotal);
        }

        public void CancelAnAppOfPromoCode(ShoppingCart shoppingCart)
        {
            ArgumentNullException.ThrowIfNull((shoppingCart));

            shoppingCart.ActualSubTotal = shoppingCart.SubTotal;
            shoppingCart.IsPromoCodeApplied = false;

            //update the total fees of the order
            ShoppingCartCostsManager.UpdateTheTotalFeesOfTheCartOrder(shoppingCart.ActualSubTotal);

            _unitOfWork.Save();
        }

        /// <summary>
        /// return the promo code id for the correct number of the shopping cart products
        /// </summary>
        /// <param name="numberOfShoppingCartProducts"></param>
        /// <returns></returns>
        public static int? GetShoppingCartPromoCode(int numberOfShoppingCartProducts)
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

        public static decimal PromoCodeValueThatAffectThePriceOfProduct(Product product,PromoCode promoCode,int numberOfProductsInTheCart)
        {
            ArgumentNullException.ThrowIfNull(product);

            ArgumentNullException.ThrowIfNull(promoCode);

            // check this calculation because i am not sure from it !

            return promoCode.CodeID switch
            {
                (int)ePromoCodes.SHUBHO20 => product.Price * 0.2m,
                (int)ePromoCodes.SHUBHO30 => product.Price * 0.3m,
                (int)ePromoCodes.SHUBHO40 => product.Price * 0.4m,
                _ => throw new PromoCodeException("Invalid PromoCode")
            };
        }

    }
}
