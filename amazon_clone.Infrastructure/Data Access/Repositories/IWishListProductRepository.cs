using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IWishListProductRepository : IRepository<WishListProduct>
    {
        void Update(WishList entity);
    }
}
