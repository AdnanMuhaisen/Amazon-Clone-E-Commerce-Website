using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface ICustomerProductRepository : IRepository<CustomerProduct>
    {

        void Update(CustomerProduct entity);
  
    }
}
