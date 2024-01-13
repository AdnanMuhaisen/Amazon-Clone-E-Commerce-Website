namespace amazon_clone.Models.Models
{
    public class WishList
    {
        public int WishListID { get; set; }

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();

        public ICollection<CustomerProduct>? Products { get; set; } = new List<CustomerProduct>();
        public ICollection<WishListProduct>? WishListsProducts { get; set; } = new List<WishListProduct>();

        public CustomerApplicationUser Customer { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not WishList) return false;
            if (ReferenceEquals(this, obj)) return true;

            return this.WishListID == ((WishList)obj).WishListID;
        }

        public override int GetHashCode()
        {
            return 7 * 23 + WishListID.GetHashCode();
        }
    }
}
