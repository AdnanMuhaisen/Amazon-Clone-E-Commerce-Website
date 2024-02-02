using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Infrastructure.Data_Access.Repositories
{
    public interface IAdministratorsOperationsRepository : IRepository<AdministratorOperation>
    {
        void Update(AdministratorOperation administratorOperation);
    }
}
