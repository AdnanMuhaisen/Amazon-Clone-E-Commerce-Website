using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;

namespace amazon_clone.Infrastructure.DataAccess.Repositories 
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
