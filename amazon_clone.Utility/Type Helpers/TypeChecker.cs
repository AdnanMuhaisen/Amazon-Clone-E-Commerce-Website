using Microsoft.IdentityModel.Abstractions;

namespace amazon_clone.Utility.Type_Helpers
{
    public class TypeChecker
    {
        //Test this function.
        public static bool IsContains(Type Parent,string ChildName)
        {
            ArgumentNullException.ThrowIfNull(Parent);

            ArgumentNullException.ThrowIfNull(ChildName);

            return Parent
                .GetProperties()
                .Where(p => p.Name == ChildName)
                .Any();
        }
    }
}
