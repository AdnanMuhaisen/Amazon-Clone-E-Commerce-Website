using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.Data_Access.Repositories;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Services
{
    public class AdministratorOperationService : IAdministratorOperationService
    {
        public IDashboardUnitOfWork _unitOfWork { get; }

        public AdministratorOperationService(IDashboardUnitOfWork dashboardUnitOfWork)
        {
            _unitOfWork = dashboardUnitOfWork;
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

        public void AddNewAdministratorOperation(IDashboardUnitOfWork unitOfWork, string administratorID, DateTime operationDateTime, string operationLog)
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
