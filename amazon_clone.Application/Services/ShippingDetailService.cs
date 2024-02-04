using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Services
{
    public class ShippingDetailService : IShippingDetailService
    {
        public IAppUnitOfWork _unitOfWork { get; }

        public ShippingDetailService(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddNewShippingDetail(Order targetOrder, ShippingDetail shippingDetail)
        {
            ArgumentNullException.ThrowIfNull(nameof(targetOrder));

            targetOrder!.ShippingDetails = shippingDetail;

            _unitOfWork.ShippingDetailRepository.Add(shippingDetail);

            targetOrder.StatusID = (int)eOrderStatuses.SHIPPED;

            _unitOfWork.Save();
        }

    }
}
