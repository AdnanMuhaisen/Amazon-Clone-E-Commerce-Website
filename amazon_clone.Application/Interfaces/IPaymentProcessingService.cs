using amazon_clone.Application.Managers;
using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface IPaymentProcessingService : IScopedService
    {
        IUnitOfWork _unitOfWork { get; }
        IProductQuantityManager ProductQuantityManager { get; }

        void PayByCashOnDelivery(Order order);
        void PayByCreditOrDebitCard(Order order);
    }
}