using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Models;

namespace amazon_clone.Application.Managers
{
    public class ShoppingCartSubTotalManager : IShoppingCartSubTotalManager
    {
        public void CalculateSubTotalAfterApplyingPromoCodeOnShoppingCart(ShoppingCart shoppingCart)
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

        public void UpdateTheActualSubTotalOfAShoppingCart(ShoppingCart shoppingCart)
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
