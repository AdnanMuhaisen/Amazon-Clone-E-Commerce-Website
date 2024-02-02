using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface ICustomerManager : IScopedService
    {
        IUnitOfWork UnitOfWork { get; }

        IEnumerable<CustomerApplicationUser> GetAllCustomers();
        void RemoveCustomer(string CustomerID);
    }
}