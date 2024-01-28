namespace amazon_clone.Domain.Models
{
    public class WishListProduct
    {
        public int ID { get; set; }
        public int ListID { get; set; }
        public int ProductID { get; set; }
        public WishList WishList { get; set; } = null!;
        public CustomerProduct CustomerProduct { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is not WishListProduct) return false;
            if (ReferenceEquals(this, obj)) return true;

            var targetObj = obj as WishListProduct;
            return ID == targetObj!.ID
                && ListID == targetObj.ListID
                && ProductID == targetObj.ProductID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
