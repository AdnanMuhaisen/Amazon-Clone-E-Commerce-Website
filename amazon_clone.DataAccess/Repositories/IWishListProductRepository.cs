using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IWishListProductRepository : IRepository<WishListProduct>
    {
        void Update(WishList entity);
    }
}
