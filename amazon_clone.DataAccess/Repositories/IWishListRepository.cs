using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IWishListRepository : IRepository<WishList>
    {
        void Update(WishList entity);
    }
}
