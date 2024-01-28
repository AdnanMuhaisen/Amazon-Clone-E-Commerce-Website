using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Domain.View_Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.web.Controllers
{
    public class ShippingDetailsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShippingDetailService shippingDetailService;

        public ShippingDetailsController(IUnitOfWork unitOfWork,IShippingDetailService shippingDetailService)
        {
            _unitOfWork = unitOfWork;
            this.shippingDetailService = shippingDetailService;
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
                targetOrderShippingDetails = new Domain.Models.ShippingDetail();
            }

            return View(new ShippingDetailsViewModel(OrderID, targetOrderShippingDetails));
        }


        [HttpPost]
        public IActionResult Index(ShippingDetail shippingDetail)
        {
            if (ModelState.IsValid)
            {
                var targetOrder = _unitOfWork
                    .OrderRepository
                    .Get(filter: x => x.CustomerID == CurrentCustomer.UserID && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING));

                ArgumentNullException.ThrowIfNull(targetOrder);

                shippingDetailService.AddNewShippingDetail(targetOrder, shippingDetail);
                
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
