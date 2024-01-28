using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public class ShoppingCartProductRepository : Repository<ShoppingCartProduct>, IShoppingCartProductRepository
    {
        public ShoppingCartProductRepository(AppDbContext context):base(context) { }
        
        public void Update(ShoppingCartProduct entity)
        {
            _context.Update(entity);
        }
    }
}
