using amazon_clone.DataAccess.Data;
using amazon_clone.Models.General_Database_Settings;
using amazon_clone.Utility.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.DataAccess.Repositories
{
    public class DbSettingsRepository : Repository<DbSettings>, IDbSettingsRepository
    {
        public DbSettingsRepository(AppDbContext context):base(context) { } 
        
        public new void Add(DbSettings dbSettings)
        {
            if(GetAllAsNoTracking()?.Count() > 1)
            {
                throw new InvalidInsertOperationException(nameof(dbSettings));
            }
            base.Add(dbSettings);
        }

        public void Update(DbSettings entity)
        {
            _context.Update(entity);
        }
    }
}
