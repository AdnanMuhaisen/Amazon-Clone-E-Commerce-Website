using amazon_clone.DataAccess.Data.Contexts;
using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public class AdministratorsTransactionsRepository : Repository<AdministratorTransaction>, IAdministratorsTransactionsRepository
    {
        public AdministratorsTransactionsRepository(DashboardDbContext dashboardDbContext) : base(dashboardDbContext) { }
        
        public void Update(AdministratorTransaction administratorTransaction)
        {
            _context.Update(administratorTransaction);
        }
    }
}
