using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public class PersonGenderRepository : Repository<PersonGender>, IPersonGenderRepository
    {
        public PersonGenderRepository(AppDbContext context):base(context) { }
        
        public void Update(PersonGender entity)
        {
            _context.Update(entity);
        }
    }
}
