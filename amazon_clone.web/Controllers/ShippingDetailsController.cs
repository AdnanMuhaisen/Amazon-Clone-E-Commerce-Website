using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.web.Controllers
{
    public class ShippingDetailsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShippingDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index(int OrderID)
        {
            return View(new ShippingDetailsViewModel(OrderID, new ShippingDetail()));
        }


        [HttpPost]
        public IActionResult Index(ShippingDetail shippingDetail)
        {
            return (ModelState.IsValid) ? RedirectToAction("Index", "Payment") : NotFound();
        }
    }
}
