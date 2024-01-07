using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context):base(context) { }
        
        public void Update(Order entity)
        {
            _context.Update(entity);
        }
    }
}
