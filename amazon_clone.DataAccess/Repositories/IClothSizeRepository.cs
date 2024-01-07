using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IClothSizeRepository : IRepository<ClothSize>
    {
        void Update(ClothSize entity);
    }
}
