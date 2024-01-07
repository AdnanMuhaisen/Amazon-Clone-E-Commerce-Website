using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IShippingDetailRepository : IRepository<ShippingDetail>
    {
        void Update(ShippingDetail entity);
    }
}
