using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface IWishListService : IScopedService
    {
        IAppUnitOfWork _unitOfWork { get; }

        void AddProductToWishList(int ProductID);
        void CreateACustomerWishList();
        void RemoveProductFromWishlist(int ProductID);
    }
}