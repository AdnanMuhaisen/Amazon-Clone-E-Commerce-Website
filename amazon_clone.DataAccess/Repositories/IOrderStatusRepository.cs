using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IOrderStatusRepository
    {
        void Update(OrderStatus entity);
    }
}
