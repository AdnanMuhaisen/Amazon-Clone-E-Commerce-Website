namespace amazon_clone.Models.Models
{
    public class ShoppingCartProduct
    {
        public int ShoppingCartID { get; set; }
        public int CustomerProductID { get; set; }
        public int Quantity { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = null!;
        public CustomerProduct CustomerProduct { get; set; } = null!;
    }
}
