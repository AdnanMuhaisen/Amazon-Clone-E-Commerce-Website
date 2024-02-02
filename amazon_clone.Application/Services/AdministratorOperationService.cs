using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Services
{
    public class AdministratorOperationService : IAdministratorOperationService
    {
        public IUnitOfWork _unitOfWork { get; }

        public AdministratorOperationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AdministratorOperation> GetAllAdministratorOperations()
        {
            var adminsOperations = _unitOfWork
                .AdministratorsOperationsRepository
                .GetAllAsNoTracking();

            if (adminsOperations is null)
            {
                adminsOperations = Enumerable.Empty<AdministratorOperation>();
            }

            return adminsOperations.ToList();
        }

        public void AddNewAdministratorOperation(IUnitOfWork unitOfWork, string administratorID, DateTime operationDateTime, string operationLog)
        {
            var operation = new AdministratorOperation
            {
                AdministratorID = administratorID,
                OperationDateTime = operationDateTime,
                OperationLog = operationLog
            };

            _unitOfWork.AdministratorsOperationsRepository.Add(operation);
        }
    }
}
