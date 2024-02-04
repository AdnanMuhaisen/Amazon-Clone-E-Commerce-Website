using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface IShoppingCartService : IScopedService
    {
        IAppUnitOfWork _unitOfWork { get; }

        void AddProductToShoppingCart(int ProductID);
        void RemoveProductFromShoppingCart(int CustomerProductID);
    }
}