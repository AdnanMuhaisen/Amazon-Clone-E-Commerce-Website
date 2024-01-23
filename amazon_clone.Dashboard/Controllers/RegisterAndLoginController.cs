using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.Dashboard.Controllers
{
    public class RegisterAndLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
