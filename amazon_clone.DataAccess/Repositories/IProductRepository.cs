using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product entity);
    }
}
