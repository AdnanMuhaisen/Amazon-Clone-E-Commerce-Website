using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.App_Details;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.Linq.Expressions;

namespace amazon_clone.Application.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        public IAppUnitOfWork _unitOfWork { get; }
        public OrderProcessingService(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Order> GetAllPaidOrders()
        {
            var paidCustomerOrders = _unitOfWork
                .OrderRepository
                .GetAllAsNoTracking(filter: x => x.CustomerID == CurrentCustomer.UserID
                && x.StatusID == (int)eOrderStatuses.DELIVERED,
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts)
                .Include(x => x.OrderStatus));

            if (paidCustomerOrders is null)
            {
                paidCustomerOrders = Enumerable.Empty<Order>();
            }

            return paidCustomerOrders.ToList();
        }

        public Order? GetOrder(Expression<Func<Order, bool>> filter)
        {
            var targetOrder = _unitOfWork
                .OrderRepository
                .Get(filter);

            return targetOrder;
        }

        public Session OrderCheckout()
        {
            var targetOrderToPayFor = _unitOfWork
                .OrderRepository
                .GetAll(filter: x => x.CustomerID == CurrentCustomer.UserID && x.StatusID == (int)eOrderStatuses.SHIPPED,
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts)
                .Include(i => i.ShoppingCart)
                .ThenInclude(i => i.CartPromoCode)
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
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
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
                        // the UnitAmountDecimal take the amount in cents

                        UnitAmountDecimal = ProductPriceCalculator
                        .CalculateProductPrice(targetOrderToPayFor.ShoppingCart,product),
                        Currency = "usd",
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

            options.LineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    // the UnitAmountDecimal take the amount in cents
                    UnitAmountDecimal = (StaticDetails.ORDER_DELIVERY + StaticDetails.ORDER_TAX) * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Tax",
                        Description = "Order Tax And Delivery Tax"
                    }
                },
                Quantity = 1
            });

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }

        public void CreateANewOrderWithShoppingCart()
        {
            ShoppingCart shoppingCart = new ShoppingCart
            {
                PromoCodeID = null,
                SubTotal = 0,
                ActualSubTotal = 0
            };
            _unitOfWork.ShoppingCartRepository.Add(shoppingCart);
            _unitOfWork.Save();

            int targetShoppingCartID = _unitOfWork
                .ShoppingCartRepository
                .GetAllAsNoTracking()!
                .Select(x => x.ShoppingCartID)
                .MaxBy(x => x);

            ArgumentNullException.ThrowIfNull(nameof(targetShoppingCartID));

            var customerOrder = new Order
            {
                OrderDateTime = DateTime.Now,
                CustomerID = CurrentCustomer.UserID!,
                StatusID = (int)eOrderStatuses.PROCESSING,
                ShoppingCartID = targetShoppingCartID
            };
            customerOrder.Tax = StaticDetails.ORDER_TAX;
            customerOrder.delivery = StaticDetails.ORDER_DELIVERY;
            customerOrder.Total = customerOrder.Tax + customerOrder.delivery;

            _unitOfWork.OrderRepository.Add(customerOrder);
            _unitOfWork.Save();
        }
    }
}
