using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface ICustomerProductService : IScopedService
    {
        IUnitOfWork _unitOfWork { get; }

        IEnumerable<CustomerProduct> GetAllCustomerProducts();
        IEnumerable<CustomerProduct> GetAllElectronicProducts();
        IEnumerable<CustomerProduct> GetAllJewelryProducts();
        CustomerProduct GetByID(int ProductID);
        IEnumerable<CustomerProduct> GetSearchResult(string valueToSearch);
    }
}