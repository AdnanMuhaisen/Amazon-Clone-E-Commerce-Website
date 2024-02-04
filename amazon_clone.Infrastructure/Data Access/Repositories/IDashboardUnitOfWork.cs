using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Infrastructure.Data_Access.Repositories
{
    public interface IDashboardUnitOfWork
    {
        public IAdministratorRepository AdministratorRepository { get; set; }
        public IAdministratorsOperationsRepository AdministratorsOperationsRepository { get; set; }

        void Save();
    }
}
