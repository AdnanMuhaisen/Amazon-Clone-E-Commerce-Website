using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;
namespace amazon_clone.Infrastructure.DataAccess.Repositories
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
