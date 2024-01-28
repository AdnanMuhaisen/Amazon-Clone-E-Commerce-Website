using amazon_clone.Application.Managers;
using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface IPromoCodeService : IScopedService
    {
        IUnitOfWork _unitOfWork { get; }
        IShoppingCartCostsManager ShoppingCartCostsManager { get; }

        void ApplyPromoCodeOnAShoppingCart(ShoppingCart shoppingCart);
        void ApplyShoppingCartPromoCode(string PromoCodeToApply);
        int? GetShoppingCartPromoCode(int numberOfShoppingCartProducts);
        void CancelAnAppOfPromoCode(ShoppingCart shoppingCart);
    }
}