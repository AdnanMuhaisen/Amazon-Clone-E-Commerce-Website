using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public class ClothesProductRepository : Repository<ClothesProduct>, IClothesProductRepository
    {
        public ClothesProductRepository(AppDbContext context) : base(context) { }
        
        public void Update(ClothesProduct entity)
        {
            _context.Update(entity);
        }
    }
}
