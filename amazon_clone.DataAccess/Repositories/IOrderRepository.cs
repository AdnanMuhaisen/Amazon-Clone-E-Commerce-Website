using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order entity);
    }
}
