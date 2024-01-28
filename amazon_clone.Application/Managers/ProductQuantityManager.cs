using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;

namespace amazon_clone.Application.Managers
{
    public class ProductQuantityManager : IProductQuantityManager
    {
        public void DecreaseTheProductsQuantityInAShoppingCart(ShoppingCart Cart, Dictionary<int, int> ProductsQuantities)
        {
            ArgumentNullException.ThrowIfNull(Cart);

            ArgumentNullException.ThrowIfNull(Cart.CartProducts);

            foreach (var product in Cart.CartProducts)
            {
                if (ProductsQuantities.TryGetValue(product.ProductID, out int quantityToSubtract))
                {
                    product.Quantity -= quantityToSubtract;
                }
                else
                {
                    throw new InvalidOperationException("can not decrease a quantity that is greater than the product quantity");
                }
            }
        }
    }
}
