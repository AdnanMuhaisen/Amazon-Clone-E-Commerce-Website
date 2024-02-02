using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Infrastructure.Data_Access.Repositories
{
    public class AdministratorsOperationsRepository : Repository<AdministratorOperation>, IAdministratorsOperationsRepository
    {
        public AdministratorsOperationsRepository(DashboardDbContext dashboardDbContext) : base(dashboardDbContext) { }

        public void Update(AdministratorOperation administratorOperation)
        {
            _context.Update(administratorOperation);
        }
    }
}
