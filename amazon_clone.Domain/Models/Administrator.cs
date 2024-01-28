using amazon_clone.Utility.App_Details;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Domain.Models
{
    public class Administrator : IdentityUser
    {
        [Url]
        public string ImageUrl { get; set; } = StaticDetails.DEFAULT_USER_IMAGE_PATH;


        public ICollection<AdministratorTransaction>? AdministratorTransactions { get; set; } = new List<AdministratorTransaction>();
    }
}
