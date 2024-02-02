using amazon_clone.Domain.Models;
using amazon_clone.Domain.View_Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;

namespace amazon_clone.Application.Interfaces
{
    public interface ICustomerProductService : IScopedService
    {
        IUnitOfWork _unitOfWork { get; }

        IEnumerable<CustomerProduct> GetAllCustomerProducts();
        IEnumerable<CustomerProduct> GetAllCustomerProductsWithAllDetails();
        IEnumerable<CustomerProduct> GetAllElectronicProducts();
        IEnumerable<CustomerProduct> GetAllJewelryProducts();
        CustomerProduct GetByID(int ProductID);
        CustomerProductViewModel GetCustomerProductForUpsert(int? ProductID);
        IEnumerable<CustomerProduct> GetSearchResult(string valueToSearch);
        void RemoveCustomerProduct(int ProductID);
        void UpsertCustomerProduct(CustomerProduct customerProduct, IFormFile formFile);
    }
}