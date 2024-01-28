using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IPersonGenderRepository : IRepository<PersonGender>
    {
        void Update(PersonGender entity);
    }
}
