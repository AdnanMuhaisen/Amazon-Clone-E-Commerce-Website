using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
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
