using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.Dashboard.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IAppUnitOfWork unitOfWork;
        private readonly ICustomerManager customerManager;

        public CustomersController(IAppUnitOfWork unitOfWork,ICustomerManager customerManager)
        {
            this.unitOfWork = unitOfWork;
            this.customerManager = customerManager;
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

            customerManager.RemoveCustomer(CustomerID);

            return RedirectToAction("Index");
        }
    }
}
