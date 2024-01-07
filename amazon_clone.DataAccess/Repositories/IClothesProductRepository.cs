using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IClothesProductRepository : IRepository<ClothesProduct>
    {
        void Update(ClothesProduct entity);
    }
}
