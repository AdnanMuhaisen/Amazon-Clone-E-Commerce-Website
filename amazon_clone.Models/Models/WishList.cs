namespace amazon_clone.Models.Models
{
    public class WishList
    {
        public int WishListID { get; set; }

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();

        public ICollection<CustomerProduct>? Products { get; set; } = new List<CustomerProduct>();
        public ICollection<WishListProduct>? WishListsProducts { get; set; } = new List<WishListProduct>();


        public string CustomerID { get; set; } = null!;
        public CustomerApplicationUser Customer { get; set; } = null!;
    }
}
