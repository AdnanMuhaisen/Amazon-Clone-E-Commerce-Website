using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart entity);
    }
}
