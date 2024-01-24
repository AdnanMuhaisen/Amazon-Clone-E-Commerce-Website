using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Users.CurrentUsers;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.Dashboard.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAdministratorTransactionController administratorTransactionController;

        public CustomersController(
            IUnitOfWork unitOfWork,
            IAdministratorTransactionController administratorTransactionController
            )
        {
            this.unitOfWork = unitOfWork;
            this.administratorTransactionController = administratorTransactionController;
        }

        public IActionResult Index()
        {
            var allCustomers = unitOfWork
                .UsersRepository
                .GetAllAsNoTracking()?
                .ToList();

            ArgumentNullException.ThrowIfNull(allCustomers);

            return View(allCustomers);
        }

        [HttpGet]
        public IActionResult RemoveCustomer(string CustomerID)
        {
            if(string.IsNullOrWhiteSpace(CustomerID))
            {
                return NotFound();
            }

            var targetCustomer = unitOfWork
                .UsersRepository
                .Get(filter: x => x.Id == CustomerID);

            ArgumentNullException.ThrowIfNull(targetCustomer);

            return View(targetCustomer);
        }

        [HttpPost,ActionName("RemoveCustomer")]
        public IActionResult RemoveCustomerPost(string CustomerID)
        {
            if (string.IsNullOrWhiteSpace(CustomerID))
            {
                return NotFound();
            }

            var targetCustomerToDelete = unitOfWork
                .UsersRepository
                .Get(x => x.Id == CustomerID);

            ArgumentNullException.ThrowIfNull(targetCustomerToDelete);

            try
            {
                unitOfWork.UsersRepository.Remove(targetCustomerToDelete);

                administratorTransactionController.AddNewAdministratorTransaction(
                    unitOfWork,
                    CurrentAdministrator.UserID!,
                    $"Delete the customer : Customer Name : {targetCustomerToDelete.UserName}",
                    DateTime.Now
                    );

                unitOfWork.Save();
            }
            catch (Exception) 
            {
                administratorTransactionController.AddNewAdministratorTransaction(
                    unitOfWork,
                    CurrentAdministrator.UserID!,
                    $"Try to delete the customer : Customer Name : {targetCustomerToDelete.UserName}",
                    DateTime.Now
                    );

                unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
