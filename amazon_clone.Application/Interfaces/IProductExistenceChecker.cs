using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface IProductExistenceChecker : IScopedService
    {
        IAppUnitOfWork _unitOfWork { get; }

        bool IsProductInTheCustomerShoppingCart(int ProductID);
        bool IsProductInTheCustomerWishlist(int ProductID);
    }
}