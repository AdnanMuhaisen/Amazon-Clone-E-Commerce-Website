using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Users;
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
            var targetOrderShippingDetails = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.OrderID == OrderID, IncludeProperties: "ShippingDetails")?
                .ShippingDetails;

            if(targetOrderShippingDetails is null)
            {
                targetOrderShippingDetails = new Models.Models.ShippingDetail();
            }

            return View(new ShippingDetailsViewModel(OrderID, targetOrderShippingDetails));
        }


        [HttpPost]
        public IActionResult Index(Models.Models.ShippingDetail shippingDetail)
        {
            if (ModelState.IsValid)
            {
                var targetOrder = _unitOfWork
                    .OrderRepository
                    .Get(filter:x => x.CustomerID == CurrentCustomer.UserID && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING));

                ArgumentNullException.ThrowIfNull(nameof(targetOrder));

                targetOrder!.ShippingDetails = shippingDetail;
                _unitOfWork.ShippingDetailRepository.Add(shippingDetail);
                targetOrder.StatusID = (int)eOrderStatuses.SHIPPED;

                _unitOfWork.Save();

                return RedirectToAction("Index", "Payment", routeValues: targetOrder.OrderID);
            }
            else
            {
                ModelState.AddModelError("", "Enter a valid data");
                return View();
            }
        }
    }
}
