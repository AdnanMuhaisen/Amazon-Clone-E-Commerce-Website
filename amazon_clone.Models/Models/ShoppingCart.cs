namespace amazon_clone.Models.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartID { get; set; }

        public decimal SubTotal { get; set; } = 0m;

        public decimal? SubTotalAfterApplyingPromoCode { get; set; } = null!;

        public bool IsPromoCodeApplied { get; set; } = false;

        //this to determine the final subtotal after apply or not apply the promo code
        public decimal ActualSubTotal { get; set; }

        public ICollection<CustomerProduct> CartProducts { get; set; } = new List<CustomerProduct>();
        public ICollection<ShoppingCartProduct> ShoppingCartsProducts { get; set; } = new List<ShoppingCartProduct>();

        public int? PromoCodeID { get; set; }
        public PromoCode? CartPromoCode { get; set; } = null!;

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();

        public Order Order { get; set; } = null!;
    }
}
