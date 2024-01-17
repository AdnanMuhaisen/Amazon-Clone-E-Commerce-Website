using amazon_clone.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.web.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index(ShippingDetail shippingDetail)
        {
            ArgumentNullException.ThrowIfNull(nameof(shippingDetail));
            return View(shippingDetail);
        }

        public IActionResult CashOnDeliveryPaymentMethod(ShippingDetail shippingDetail)
        {




            return View(shippingDetail);
        }
    }
}
