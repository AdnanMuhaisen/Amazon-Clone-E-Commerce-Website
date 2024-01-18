using amazon_clone.Models.Models;
using amazon_clone.Utility.Type_Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace amazon_clone.DataAccess.Interceptors
{
    public class UpdateCreationAndUpdateDetailsInterceptor : SaveChangesInterceptor
    {
        //test this method
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if(eventData.Context is null)
            {
                return result;
            }
            // get the child name
            string ChildName = typeof(CreationDetails).Name;

            // iterate all the tracked entities that contains the target type
            foreach (var entry in eventData.Context.ChangeTracker.Entries()) 
            {
                // get all the modified entities
                if (entry.State is EntityState.Modified)
                {
                    // update the object value
                    Type type = entry.Entity.GetType();
                    if (TypeChecker.IsContains(type, ChildName))
                    {
                        var targetObjectPropertyValue = type.GetProperty(ChildName)?.GetValue(entry.Entity);

                        if(targetObjectPropertyValue is null)
                        {
                            throw new NullReferenceException(nameof(targetObjectPropertyValue));
                        }

                        var objectPropertyValue = targetObjectPropertyValue as CreationDetails;

                        if (objectPropertyValue is null)
                        {
                            throw new NullReferenceException(nameof(objectPropertyValue));
                        }

                        objectPropertyValue.UpdatedAt = DateTime.Now;
                        type.GetProperty(ChildName)?.SetValue(objectPropertyValue, entry.Entity);
                    }
                }
            }
            return result;
        }
    }
}
