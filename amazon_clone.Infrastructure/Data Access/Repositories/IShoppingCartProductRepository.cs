using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IShoppingCartProductRepository : IRepository<ShoppingCartProduct>
    {
        void Update(ShoppingCartProduct entity);
    }
}
