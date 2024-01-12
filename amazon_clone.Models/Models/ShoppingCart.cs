namespace amazon_clone.Models.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartID { get; set; }


        public ICollection<CustomerProduct> CartProducts { get; set; } = new List<CustomerProduct>();
        public ICollection<ShoppingCartProduct> ShoppingCartsProducts { get; set; } = new List<ShoppingCartProduct>();

        public int? PromoCodeID { get; set; }
        public PromoCode? CartPromoCode { get; set; } = null!;

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();

        public CustomerApplicationUser Customer { get; set; } = null!;
    }
}
