using amazon_clone.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.Dashboard.Controllers
{
    public class AdministratorOperationsController : Controller
    {
        private readonly IAdministratorOperationService administratorOperationService;

        public AdministratorOperationsController(IAdministratorOperationService administratorOperationService)
        {
            this.administratorOperationService = administratorOperationService;
        }

        public IActionResult Index()
        {
            var allOperations = administratorOperationService
                .GetAllAdministratorOperations();

            return View(allOperations);
        }
    }
}
