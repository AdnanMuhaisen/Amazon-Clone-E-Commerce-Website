using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Domain.Models
{
    public class Product : ISoftDeletable
    {
        [ValidateNever, Key]
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
            return ProductID == targetProduct!.ProductID
                && Name == targetProduct.Name
                && Description == targetProduct.Description
                && Price == targetProduct.Price
                && Quantity == targetProduct.Quantity
                && ImageUrl == targetProduct.ImageUrl;
        }

        public override int GetHashCode()
        {
            var hash = 7;
            //hash*=7+
            hash *= 23 + ProductID.GetHashCode();
            hash *= 23 + Name.GetHashCode();
            hash *= 23 + Description.GetHashCode();
            hash *= 23 + Price.GetHashCode();
            hash *= 23 + Quantity.GetHashCode();
            hash *= 23 + ImageUrl!.GetHashCode();
            return hash;
        }
    }
}
