using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface IProductCategoryManager : IScopedService
    {
        IUnitOfWork UnitOfWork { get; }

        IEnumerable<ProductCategory> GetAllCategories();
        void RemoveProductCategory(int CategoryID);
        void UpsertProductCategory(ProductCategory productCategory);
    }
}