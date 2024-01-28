using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public class ClothesProductRepository : Repository<ClothingProduct>, IClothesProductRepository
    {
        public ClothesProductRepository(AppDbContext context) : base(context) { }
        
        public void Update(ClothingProduct entity)
        {
            _context.Update(entity);
        }
    }
}
