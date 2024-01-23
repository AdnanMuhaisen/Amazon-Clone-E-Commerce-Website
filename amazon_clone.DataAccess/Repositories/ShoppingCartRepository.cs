using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.DataAccess.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(AppDbContext context):base(context) { }
        
        public void Update(ShoppingCart entity)
        {
            _context.Update(entity);
        }
    }
}
