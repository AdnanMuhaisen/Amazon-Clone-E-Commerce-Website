using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IPersonGenderRepository : IRepository<PersonGender>
    {
        void Update(PersonGender entity);
    }
}
