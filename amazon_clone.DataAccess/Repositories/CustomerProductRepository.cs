using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public class CustomerProductRepository : Repository<CustomerProduct>, ICustomerProductRepository
    {
        public CustomerProductRepository(AppDbContext context):base(context) { }
        
        public void Update(CustomerProduct entity)
        {
            throw new NotImplementedException();
        }
    }
}
