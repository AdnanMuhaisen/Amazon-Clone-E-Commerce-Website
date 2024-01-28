using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface ICustomerProductRepository : IRepository<CustomerProduct>
    {

        void Update(CustomerProduct entity);
  
    }
}
