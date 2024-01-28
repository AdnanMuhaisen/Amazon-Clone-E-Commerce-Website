using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Domain.Models
{
    public class CustomerApplicationUser : IdentityUser
    {
        [Url]
        public string ImageUrl { get; set; } = null!;

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();

        //relationships
        public int? WishListID { get; set; }
        public WishList WishList { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Payment> CustomerPayments { get; set; } = null!;
    }
}
