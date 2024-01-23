using amazon_clone.DataAccess.Data.Contexts;
using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.DataAccess.Repositories
{
    public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
    {

        public AdministratorRepository(DashboardDbContext context):base(context)
        {
            
        }

        public void Update(Administrator administrator)
        {
            _context.Update(administrator);
        }
    }
}
