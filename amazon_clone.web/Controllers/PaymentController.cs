using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users.CurrentUsers;
using amazon_clone.Models.View_Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace amazon_clone.web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public PaymentController(IUnitOfWork unitOfWork,IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var targetOrder = _unitOfWork
                .OrderRepository
                .Get(x => (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING)
                && x.CustomerID == CurrentCustomer.UserID);

            ArgumentNullException.ThrowIfNull(nameof(targetOrder));

            return View(new PaymentViewModel(targetOrder!.Total, null!));
        }

        [HttpPost,ActionName("Index")]
        public IActionResult IndexPost(string paymentMethod)
        {
            ArgumentNullException.ThrowIfNull(nameof(paymentMethod));

            if (paymentMethod == ePaymentMethods.CASH_ON_DELIVERY.ToString())
            {
                return RedirectToAction("CashOnDeliveryPaymentMethod");
            }
            else if (paymentMethod == ePaymentMethods.CREDIT_OR_DEBIT_CARD.ToString())
            {
                return RedirectToAction("OrderCheckout", "Order");
            }

            return NotFound();
        }

        public IActionResult CashOnDeliveryPaymentMethod()
        {
            var targetOrderToPayFor = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID && x.StatusID == (int)eOrderStatuses.SHIPPED,
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts));

            ArgumentNullException.ThrowIfNull((targetOrderToPayFor));

            ArgumentNullException.ThrowIfNull(targetOrderToPayFor.ShoppingCart);

            ArgumentNullException.ThrowIfNull(targetOrderToPayFor.ShoppingCart.CartProducts);

            var cartProductsQuantities = _unitOfWork
                .ShoppingCartProductRepository
                .GetAllAsNoTracking(x => x.ShoppingCartID == targetOrderToPayFor.ShoppingCartID)?
                .Select(x => new
                {
                    CustomerProductID = x.CustomerProductID,
                    Quantity = x.Quantity
                })
                .ToDictionary(k => k.CustomerProductID, v => v.Quantity);

            ArgumentNullException.ThrowIfNull(cartProductsQuantities);

            Payment payment = new Payment
            {
                PaymentMethod = ePaymentMethods.CASH_ON_DELIVERY.ToString(),
                PaymentDateTime = DateTime.Now,
                TotalAmount = targetOrderToPayFor!.Total,
                CustomerID = CurrentCustomer.UserID!,
                OrderID = targetOrderToPayFor.OrderID
            };
            try
            {
                targetOrderToPayFor.StatusID = (int)eOrderStatuses.DELIVERED;

                _unitOfWork.PaymentRepository.Add(payment);
                _unitOfWork.OrderRepository.Update(targetOrderToPayFor);

                _DecreaseTheProductsQuantityInAShoppingCart(targetOrderToPayFor.ShoppingCart, cartProductsQuantities);

                _unitOfWork.Save();
            }
            catch (Exception) 
            {
                //log
                return RedirectToAction("PaymentFailed");            
            }

            return RedirectToAction("PaymentSuccessful", routeValues: targetOrderToPayFor.OrderID);
        }

        public IActionResult PaymentSuccessful(int OrderID)
        {
            _emailSender.SendEmailAsync(
                CurrentCustomer.Email,
                "Amazon Clone Order Notifier",
                $"You have successfully paid for the order with id {OrderID}"
                );


            return View("PaymentSuccessful");
        }
        public IActionResult PaymentFailed() => View("PaymentFailed");

        public IActionResult PaymentSuccessfulUsingCreditOrDebitCard()
        {
            var targetOrderToPayFor = _unitOfWork
                .OrderRepository
                .Get(filter: x => x.CustomerID == CurrentCustomer.UserID && x.StatusID == (int)eOrderStatuses.SHIPPED,
                 include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts));

            ArgumentNullException.ThrowIfNull((targetOrderToPayFor));

            ArgumentNullException.ThrowIfNull(targetOrderToPayFor.ShoppingCart);

            ArgumentNullException.ThrowIfNull(targetOrderToPayFor.ShoppingCart.CartProducts);

            var cartProductsQuantities = _unitOfWork
                .ShoppingCartProductRepository
                .GetAllAsNoTracking(x => x.ShoppingCartID == targetOrderToPayFor.ShoppingCartID)?
                .Select(x => new
                {
                    CustomerProductID = x.CustomerProductID,
                    Quantity = x.Quantity
                })
                .ToDictionary(k => k.CustomerProductID, v => v.Quantity);

            ArgumentNullException.ThrowIfNull(cartProductsQuantities);

            Payment payment = new Payment
            {
                PaymentMethod = ePaymentMethods.CREDIT_OR_DEBIT_CARD.ToString(),
                PaymentDateTime = DateTime.Now,
                TotalAmount = targetOrderToPayFor!.Total,
                CustomerID = CurrentCustomer.UserID!,
                OrderID = targetOrderToPayFor.OrderID
            };
            try
            {
                targetOrderToPayFor.StatusID = (int)eOrderStatuses.DELIVERED;

                _unitOfWork.PaymentRepository.Add(payment);
                _unitOfWork.OrderRepository.Update(targetOrderToPayFor);

                _DecreaseTheProductsQuantityInAShoppingCart(targetOrderToPayFor.ShoppingCart, cartProductsQuantities);

                _unitOfWork.Save();
            }
            catch (Exception)
            {
                //log
                return RedirectToAction("PaymentFailed");
            }

            return RedirectToAction("PaymentSuccessful", routeValues: targetOrderToPayFor.OrderID);
        }

        private void _DecreaseTheProductsQuantityInAShoppingCart(ShoppingCart Cart,Dictionary<int,int> ProductsQuantities)
        {
            ArgumentNullException.ThrowIfNull(Cart);

            ArgumentNullException.ThrowIfNull(Cart.CartProducts);

            foreach (var product in Cart.CartProducts) 
            {
                if (ProductsQuantities.TryGetValue(product.ProductID, out int quantityToSubtract))
                {
                    product.Quantity -= quantityToSubtract;
                }
                else
                {
                    throw new InvalidOperationException("can not decrease a quantity that is greater than the product quantity");
                }
            }
        }
    }
}
