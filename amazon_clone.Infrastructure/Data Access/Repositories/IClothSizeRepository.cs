using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IClothSizeRepository : IRepository<ClothSize>
    {
        void Update(ClothSize entity);
    }
}
