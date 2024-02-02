using amazon_clone.Presentation.CustomerApp.Filters;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.web.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
