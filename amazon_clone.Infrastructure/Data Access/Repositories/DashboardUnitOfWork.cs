using amazon_clone.Infrastructure.DataAccess.Data.Contexts;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Infrastructure.Data_Access.Repositories
{
    public class DashboardUnitOfWork(DashboardDbContext dashboardDbContext) : IDashboardUnitOfWork
    {
        public IAdministratorRepository AdministratorRepository { get; set; } = new AdministratorRepository(dashboardDbContext);
        public IAdministratorsOperationsRepository AdministratorsOperationsRepository { get; set; } = new AdministratorsOperationsRepository(dashboardDbContext);

        public void Save()
        {
            dashboardDbContext.SaveChanges();
        }
    }
}
