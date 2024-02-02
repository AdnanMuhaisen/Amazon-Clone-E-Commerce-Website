using amazon_clone.Application.Interfaces;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.Dashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboardHomePageInformationManager dashboardHomePageInformationManager;

        public HomeController(ILogger<HomeController> logger,IDashboardHomePageInformationManager dashboardHomePageInformationManager)
        {
            _logger = logger;
            this.dashboardHomePageInformationManager = dashboardHomePageInformationManager;
        }

        public IActionResult Index()
        {
            var administratorDashboardViewModel = dashboardHomePageInformationManager.CalculateHomePageData();

            return View(administratorDashboardViewModel);
        }
    }
}
