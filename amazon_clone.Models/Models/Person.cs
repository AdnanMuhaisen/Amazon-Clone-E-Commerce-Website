using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Models.Models
{
    public abstract class Person
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();
    }
}
