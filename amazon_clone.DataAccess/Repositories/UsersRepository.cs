using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.DataAccess.Repositories
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
