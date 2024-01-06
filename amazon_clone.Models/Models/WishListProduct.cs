namespace amazon_clone.Models.Models
{
    public class WishListProduct
    {
        public int ID { get; set; }
        public int ListID { get; set; }
        public int ProductID { get; set; }
        public WishList WishList { get; set; } = null!;
        public CustomerProduct CustomerProduct { get; set; } = null!;
    }
}
