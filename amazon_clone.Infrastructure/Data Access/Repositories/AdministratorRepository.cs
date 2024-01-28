using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
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
