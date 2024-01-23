using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.DataAccess.Repositories
{
    public class ShippingDetailRepository : Repository<ShippingDetail>, IShippingDetailRepository
    {
        public ShippingDetailRepository(AppDbContext context):base(context) { }
        
        public void Update(ShippingDetail entity)
        {
            _context.Update(entity);
        }
    }
}
