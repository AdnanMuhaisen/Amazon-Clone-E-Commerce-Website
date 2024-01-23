using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context) { }

        public void Update(Product entity)
        {
            _context.Update(entity);
        }
    }
}
