namespace amazon_clone.Models.Models
{
    public class CustomerProduct : Product
    {
        public int CustomerProductID { get; set; }


        public ICollection<WishList>? ProductWishLists { get; set; } = new List<WishList>();
        public ICollection<WishListProduct>? WishListsProducts { get; set; } = new List<WishListProduct>();


        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
        public ICollection<ShoppingCartProduct> ShoppingCartsProducts { get; set; } = new List<ShoppingCartProduct>();

        public override bool Equals(object? obj)
        {
            ArgumentNullException.ThrowIfNull(nameof(obj));
            if (obj is not CustomerProduct) return false;
            if (ReferenceEquals(this, obj)) return true;

            var targetObj = obj as CustomerProduct;
            return base.Equals(obj) && this.CustomerProductID == targetObj!.CustomerProductID;
        }

        public override int GetHashCode()
        {
            var hash = base.GetHashCode();
            hash *= 7 + this.CustomerProductID.GetHashCode();
            return hash;
        }
    }
}
