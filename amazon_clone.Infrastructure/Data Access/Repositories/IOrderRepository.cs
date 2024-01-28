using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order entity);
    }
}
