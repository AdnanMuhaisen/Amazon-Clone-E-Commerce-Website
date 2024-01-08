using Microsoft.AspNetCore.Identity;

namespace amazon_clone.Models.Models
{
    public abstract class Person : IdentityUser
    { 
        public CreationDetails CreationDetails { get; set; } = new CreationDetails();
    }
}
