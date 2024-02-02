using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Interfaces
{
    public interface IAdministratorOperationService : IScopedService
    {
        IUnitOfWork _unitOfWork { get; }

        void AddNewAdministratorOperation(IUnitOfWork unitOfWork, string administratorID, DateTime operationDateTime, string operationLog);
        IEnumerable<AdministratorOperation> GetAllAdministratorOperations();
    }
}