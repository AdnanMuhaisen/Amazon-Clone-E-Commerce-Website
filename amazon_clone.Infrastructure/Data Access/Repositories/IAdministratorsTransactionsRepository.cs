using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IAdministratorsTransactionsRepository : IRepository<AdministratorTransaction>
    {
        void Update(AdministratorTransaction administratorTransaction);
    }
}
