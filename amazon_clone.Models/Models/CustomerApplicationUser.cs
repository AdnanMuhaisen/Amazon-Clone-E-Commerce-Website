using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Models.Models
{
    public class CustomerApplicationUser : IdentityUser
    {
        [Url]
        public string ImageUrl { get; set; } = null!;

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();

        //relationships
        public WishList WishList { get; set; } = null!;

        public int ShoppingCartID { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
