using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace amazon_clone.DataAccess.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if(eventData.Context is null)
            {
                return result;
            }

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if(entry.Entity is null 
                    || entry.Entity is not ISoftDeletable entity
                    || !(entry.State is EntityState.Deleted))
                {
                    continue;
                }
                entry.State = EntityState.Modified;
                entity.Delete();
            }
            return result;
        }
    }
}
