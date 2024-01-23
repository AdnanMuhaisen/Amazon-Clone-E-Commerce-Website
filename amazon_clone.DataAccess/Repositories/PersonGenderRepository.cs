using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.DataAccess.Repositories
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
