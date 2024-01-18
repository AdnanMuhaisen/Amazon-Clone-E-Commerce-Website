using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Update(Payment payment);
    }
}
