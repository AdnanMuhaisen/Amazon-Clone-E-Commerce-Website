using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product entity);
    }
}
