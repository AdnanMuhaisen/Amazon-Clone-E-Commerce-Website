using amazon_clone.Domain.Models;

namespace amazon_clone.Application.Interfaces
{
    public interface IShoppingCartSubTotalManager : IScopedService
    {
        void CalculateSubTotalAfterApplyingPromoCodeOnShoppingCart(ShoppingCart shoppingCart);
        void UpdateTheActualSubTotalOfAShoppingCart(ShoppingCart shoppingCart);
    }
}