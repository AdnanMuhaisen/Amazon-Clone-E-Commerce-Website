using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.Data_Access.Repositories;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface IAdministratorOperationService : IScopedService
    {
        IDashboardUnitOfWork _unitOfWork { get; }

        void AddNewAdministratorOperation(IDashboardUnitOfWork unitOfWork, string administratorID, DateTime operationDateTime, string operationLog);
        IEnumerable<AdministratorOperation> GetAllAdministratorOperations();
    }
}