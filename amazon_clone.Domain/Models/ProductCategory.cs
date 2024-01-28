using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Domain.Models
{
    public class ProductCategory
    {
        [Key, ValidateNever]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = null!;
        public CreationDetails CategoryCreationDetails { get; set; } = new CreationDetails();

        public ICollection<Product> CategoryProducts { get; set; } = new List<Product>();
    }
}
