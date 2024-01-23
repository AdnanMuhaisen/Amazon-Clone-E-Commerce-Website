using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IAdministratorRepository : IRepository<Administrator>
    {
        void Update(Administrator administrator);
    }
}
