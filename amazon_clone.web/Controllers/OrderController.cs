using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace amazon_clone.web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var paidCustomerOrders = _unitOfWork
                .OrderRepository
                .GetAllAsNoTracking(filter: x => x.CustomerID == CurrentCustomer.UserID
                && x.StatusID == (int)eOrderStatuses.DELIVERED,
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts)
                .Include(x => x.OrderStatus))?
                .ToList();

            if(paidCustomerOrders is null)
            {
                paidCustomerOrders = new List<Order>();
            }

            return View(paidCustomerOrders);
        }

        public IActionResult OrderConfirmation()
        {
            var targetOrderToPayFor = _unitOfWork
                .OrderRepository
                .GetAll(filter: x => x.CustomerID == CurrentCustomer.UserID && x.StatusID == (int)eOrderStatuses.SHIPPED,
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts)
                .Include(x => x.ShippingDetails!))?
                .FirstOrDefault();

            ArgumentNullException.ThrowIfNull(nameof(targetOrderToPayFor));

            var cartProductsQuantities = _unitOfWork
                .ShoppingCartProductRepository
                .GetAllAsNoTracking(filter: x => x.ShoppingCartID == targetOrderToPayFor!.ShoppingCartID
                && targetOrderToPayFor
                .ShoppingCart.CartProducts
                .Select(x => x.CustomerProductID)
                .Contains(x.CustomerProductID))?
                .ToDictionary(x => x.CustomerProductID, x => x.Quantity);

            //check the quantities
            ArgumentNullException.ThrowIfNull(nameof(cartProductsQuantities));

            ArgumentNullException.ThrowIfNull(nameof(targetOrderToPayFor.ShippingDetails));

            //stripe implementation
            string domain = @"https://localhost:44340/";

            //provide the required url`s here
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + @"Payment/PaymentSuccessfulUsingCreditOrDebitCard",
                CancelUrl = domain + @"Payment/PaymentFailed",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                CustomerEmail = targetOrderToPayFor?.ShippingDetails?.EmailAddress
            };

            foreach (var product in targetOrderToPayFor!.ShoppingCart!.CartProducts)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)product.Price+1_000_000,
                        Currency = "inr",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                            Description = product.Description
                        }
                    },
                    Quantity = cartProductsQuantities![product.CustomerProductID]
                };

                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            //test this line 
            Response.Headers.Append("Location", session.Url);

            TempData["Session"] = session.Id;

            return new StatusCodeResult(303);
        }
    }
}
