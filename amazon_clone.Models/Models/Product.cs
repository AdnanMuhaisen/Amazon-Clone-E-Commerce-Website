
namespace amazon_clone.Models.Models
{
    public abstract class Product : ISoftDeletable
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public DateOnly? DeleteDate { get; set; }
        public string? ImageUrl { get; set; } = null!;
        public CreationDetails ProductCreationDetails { get; set; } = new CreationDetails();


        // Category Relationship
        public int CategoryID { get; set; }
        public ProductCategory Category { get; set; } = null!;


    }
}
