
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Models.Models
{
    public class Product : ISoftDeletable
    {
        [ValidateNever,Key]
        public int ProductID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
        [ValidateNever]
        public bool IsDeleted { get; set; }
        
        [ValidateNever]
        public DateOnly? DeleteDate { get; set; }
        public string? ImageUrl { get; set; } = null!;

        [ValidateNever]
        public CreationDetails ProductCreationDetails { get; set; } = new CreationDetails();


        // Category Relationship
        public int CategoryID { get; set; }

        [ValidateNever]
        public ProductCategory Category { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            ArgumentNullException.ThrowIfNull(nameof(obj));
            if (obj is not Product) return false;
            if (ReferenceEquals(this, obj)) return true;

            var targetProduct = obj as Product;
            return this.ProductID   == targetProduct!.ProductID
                && this.Name        == targetProduct.Name
                && this.Description == targetProduct.Description
                && this.Price       == targetProduct.Price
                && this.Quantity    == targetProduct.Quantity
                && this.ImageUrl    == targetProduct.ImageUrl;
        }

        public override int GetHashCode()
        {
            var hash = 7;
            //hash*=7+
            hash *= 23 + this.ProductID.GetHashCode();
            hash *= 23 + this.Name.GetHashCode();
            hash *= 23 + this.Description.GetHashCode();
            hash *= 23 + this.Price.GetHashCode();
            hash *= 23 + this.Quantity.GetHashCode();
            hash *= 23 + this.ImageUrl!.GetHashCode();
            return hash;
        }
    }
}
