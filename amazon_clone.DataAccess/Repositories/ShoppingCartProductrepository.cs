using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.DataAccess.Repositories
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
