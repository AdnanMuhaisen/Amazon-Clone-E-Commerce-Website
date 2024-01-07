using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    internal class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AppDbContext context) : base(context) { }
        
        public void Update(ProductCategory entity)
        {
            _context.Update(entity);
        }
    }
}
