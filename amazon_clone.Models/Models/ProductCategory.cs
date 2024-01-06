namespace amazon_clone.Models.Models
{
    public class ProductCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }=null!;
        public CreationDetails CategoryCreationDetails { get; set; } = new CreationDetails();

        public ICollection<Product> CategoryProducts { get; set; } = new List<Product>();
    }
}
