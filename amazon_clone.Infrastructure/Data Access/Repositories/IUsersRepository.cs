using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IUsersRepository : IRepository<CustomerApplicationUser>
    {
        void Update(CustomerApplicationUser customer);
    }
}
