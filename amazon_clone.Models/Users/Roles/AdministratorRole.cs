using Microsoft.AspNetCore.Identity;

namespace amazon_clone.Models.Users.Roles
{
    public class AdministratorRole : IdentityRole
    {
        public AdministratorRole()
        {
            
        }
        public AdministratorRole(string RoleName):base(roleName:RoleName) { }
    }
}
