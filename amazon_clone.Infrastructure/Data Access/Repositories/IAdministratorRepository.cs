using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IAdministratorRepository : IRepository<Administrator>
    {
        void Update(Administrator administrator);
    }
}
