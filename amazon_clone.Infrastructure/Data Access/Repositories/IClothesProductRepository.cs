using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IClothesProductRepository : IRepository<ClothingProduct>
    {
        void Update(ClothingProduct entity);
    }
}
