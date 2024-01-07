using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public class ClothSizeRepository : Repository<ClothSize>, IClothSizeRepository
    {
        public ClothSizeRepository(AppDbContext context):base(context) { }
        
        public void Update(ClothSize entity)
        {
            _context.Update(entity);
        }
    }
}
