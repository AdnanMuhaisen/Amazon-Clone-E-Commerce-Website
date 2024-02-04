using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface IShippingDetailService : IScopedService
    {
        IAppUnitOfWork _unitOfWork { get; }

        void AddNewShippingDetail(Order targetOrder, ShippingDetail shippingDetail);
    }
}