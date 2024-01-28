using amazon_clone.Domain.Models;

namespace amazon_clone.Application.Interfaces
{
    public interface IProductQuantityManager : IScopedService
    {
        void DecreaseTheProductsQuantityInAShoppingCart(ShoppingCart Cart, Dictionary<int, int> ProductsQuantities);
    }
}