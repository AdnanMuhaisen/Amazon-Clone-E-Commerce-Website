using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IAdministratorOperationService administratorOperationService;

        public IUnitOfWork UnitOfWork { get; }

        public CustomerManager(IUnitOfWork unitOfWork, IAdministratorOperationService administratorOperationService)
        {
            UnitOfWork = unitOfWork;
            this.administratorOperationService = administratorOperationService;
        }

        public IEnumerable<CustomerApplicationUser> GetAllCustomers()
        {
            var allCustomers = UnitOfWork
                .UsersRepository
                .GetAllAsNoTracking();

            if (allCustomers is null)
            {
                allCustomers = Enumerable.Empty<CustomerApplicationUser>();
            }

            return allCustomers.ToList();
        }

        public void RemoveCustomer(string CustomerID)
        {
            var targetCustomerToDelete = UnitOfWork
                .UsersRepository
                .Get(x => x.Id == CustomerID);

            ArgumentNullException.ThrowIfNull(targetCustomerToDelete);


            UnitOfWork.UsersRepository.Remove(targetCustomerToDelete);

            administratorOperationService.AddNewAdministratorOperation(
                UnitOfWork,
                CurrentAdministrator.UserID!,
                DateTime.Now,
                $"Delete the customer : Customer Name : {targetCustomerToDelete.UserName}");

            UnitOfWork.Save();
        }

    }
}
