using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Domain.Models
{
    public class CustomerProduct : Product
    {
        [ValidateNever]
        public int CustomerProductID { get; set; }

        [ValidateNever]
        public ICollection<WishList>? ProductWishLists { get; set; } = new List<WishList>();
        [ValidateNever]
        public ICollection<WishListProduct>? WishListsProducts { get; set; } = new List<WishListProduct>();

        [ValidateNever]
        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
        [ValidateNever]
        public ICollection<ShoppingCartProduct> ShoppingCartsProducts { get; set; } = new List<ShoppingCartProduct>();

        public override bool Equals(object? obj)
        {
            ArgumentNullException.ThrowIfNull(nameof(obj));
            if (obj is not CustomerProduct) return false;
            if (ReferenceEquals(this, obj)) return true;

            var targetObj = obj as CustomerProduct;
            return base.Equals(obj) && CustomerProductID == targetObj!.CustomerProductID;
        }

        public override int GetHashCode()
        {
            var hash = base.GetHashCode();
            hash *= 7 + CustomerProductID.GetHashCode();
            return hash;
        }
    }
}
