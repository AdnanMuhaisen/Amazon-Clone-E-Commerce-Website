using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IUsersRepository : IRepository<CustomerApplicationUser>
    {
        void Update(CustomerApplicationUser customer);
    }
}
