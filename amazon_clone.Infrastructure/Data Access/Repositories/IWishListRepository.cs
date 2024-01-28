using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IWishListRepository : IRepository<WishList>
    {
        void Update(WishList entity);
    }
}
