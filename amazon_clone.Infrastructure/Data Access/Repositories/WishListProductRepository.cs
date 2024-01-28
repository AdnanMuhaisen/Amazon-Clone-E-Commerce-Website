using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public class WishListProductRepository : Repository<WishListProduct>, IWishListProductRepository
    {
        public WishListProductRepository(AppDbContext context) : base(context) { }

        public void Update(WishList entity)
        {
            _context.Update(entity);
        }
    }
}
