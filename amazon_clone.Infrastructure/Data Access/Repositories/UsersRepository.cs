using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public class UsersRepository : Repository<CustomerApplicationUser>, IUsersRepository
    {
        public UsersRepository(AppDbContext context):base(context) { }  
        
        public void Update(CustomerApplicationUser customer)
        {
            _context.Update(customer);
        }
    }
}
