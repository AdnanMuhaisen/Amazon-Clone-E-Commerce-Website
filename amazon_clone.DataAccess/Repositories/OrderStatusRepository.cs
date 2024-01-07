using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    internal class OrderStatusRepository : Repository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(AppDbContext context):base(context) { }
        

        public void Update(OrderStatus entity)
        {
            _context.Update(entity);
        }
    }
}
