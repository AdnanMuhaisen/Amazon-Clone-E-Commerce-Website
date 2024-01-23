using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IAdministratorsTransactionsRepository : IRepository<AdministratorTransaction>
    {
        void Update(AdministratorTransaction administratorTransaction);
    }
}
