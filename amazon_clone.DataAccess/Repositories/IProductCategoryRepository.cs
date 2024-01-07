using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        void Update(ProductCategory entity);
    }
}
