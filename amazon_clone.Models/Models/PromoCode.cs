namespace amazon_clone.Models.Models
{
    public class PromoCode
    {
        public int CodeID { get; set; }
        public string Code { get; set; } = null!;
        public int ForQuantity { get; set; }

        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();

    }
}
