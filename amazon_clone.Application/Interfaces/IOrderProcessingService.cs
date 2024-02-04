using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Stripe.Checkout;
using System.Linq.Expressions;

namespace amazon_clone.Application.Interfaces
{
    public interface IOrderProcessingService : IScopedService
    {
        IAppUnitOfWork _unitOfWork { get; }

        void CreateANewOrderWithShoppingCart();
        IEnumerable<Order> GetAllPaidOrders();
        Order? GetOrder(Expression<Func<Order, bool>> filter);
        Session OrderCheckout();
    }
}