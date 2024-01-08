using amazon_clone.Models.General_Database_Settings;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IDbSettingsRepository : IRepository<DbSettings>
    {
        void Update(DbSettings entity);
    }
}
