using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IShoppingCartProductRepository : IRepository<ShoppingCartProduct>
    {
        void Update(ShoppingCartProduct entity);
    }
}
