using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IShippingDetailRepository : IRepository<ShippingDetail>
    {
        void Update(ShippingDetail entity);
    }
}
