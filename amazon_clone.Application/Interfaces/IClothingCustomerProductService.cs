using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface IClothingCustomerProductService : IScopedService
    {
        IAppUnitOfWork _unitOfWork { get; }

        ClothingProduct? Get(int ProductID, int ClothingProductID);
        IEnumerable<ClothingProduct> GetAllMenClothingProducts();
        IEnumerable<ClothingProduct> GetAllWomenClothingProducts();
    }
}