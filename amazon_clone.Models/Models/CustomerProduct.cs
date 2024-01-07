namespace amazon_clone.Models.Models
{
    public class CustomerProduct : Product
    {
        public int CustomerProductID { get; set; }


        public ICollection<WishList>? ProductWishLists { get; set; } = new List<WishList>();
        public ICollection<WishListProduct>? WishListsProducts { get; set; } = new List<WishListProduct>();


        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
        public ICollection<ShoppingCartProduct> ShoppingCartsProducts { get; set; } = new List<ShoppingCartProduct>();
    }
}
