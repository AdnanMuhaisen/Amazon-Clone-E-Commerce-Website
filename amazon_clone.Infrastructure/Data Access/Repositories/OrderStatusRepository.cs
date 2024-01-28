using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
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
