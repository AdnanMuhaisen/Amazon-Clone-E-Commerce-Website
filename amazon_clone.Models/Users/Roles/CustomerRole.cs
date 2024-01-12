using Microsoft.AspNetCore.Identity;

namespace amazon_clone.Models.Users.Roles
{
    public class CustomerRole : IdentityRole
    {
        public CustomerRole()
        {
            
        }
        public CustomerRole(string RoleName):base(RoleName) { }  
        
    }
}
