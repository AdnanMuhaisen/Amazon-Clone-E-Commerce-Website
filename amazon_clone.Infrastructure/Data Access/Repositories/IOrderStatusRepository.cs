using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IOrderStatusRepository
    {
        void Update(OrderStatus entity);
    }
}
