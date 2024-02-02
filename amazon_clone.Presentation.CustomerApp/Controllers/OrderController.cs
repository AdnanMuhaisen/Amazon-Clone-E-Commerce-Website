using amazon_clone.Application.Interfaces;
using amazon_clone.Presentation.CustomerApp.Filters;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace amazon_clone.web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderProcessingService orderProcessingService;

        public OrderController(IOrderProcessingService orderProcessingService)
        {
            this.orderProcessingService = orderProcessingService;
        }

        public IActionResult Index()
        {
            var paidCustomerOrders = orderProcessingService.GetAllPaidOrders();

            return View(paidCustomerOrders);
        }

        public IActionResult OrderCheckout()
        {
            // this method process the checkout and then return a session
            Session session = orderProcessingService.OrderCheckout();

            //test this line 
            Response.Headers.Append("Location", session.Url);

            TempData["Session"] = session.Id;

            return new StatusCodeResult(303);
        }
    }
}
