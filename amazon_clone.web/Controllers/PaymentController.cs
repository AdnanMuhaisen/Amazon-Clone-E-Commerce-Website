using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users;
using amazon_clone.Models.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace amazon_clone.web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                return RedirectToAction("OrderConfirmation", "Order");
            }

            return NotFound();
        }

        public IActionResult CashOnDeliveryPaymentMethod()
        {
            var targetOrderToPayFor = _unitOfWork
                .OrderRepository
                .Get(x => x.CustomerID == CurrentCustomer.UserID && x.StatusID == (int)eOrderStatuses.SHIPPED);

            ArgumentNullException.ThrowIfNull(nameof(targetOrderToPayFor));

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

                _unitOfWork.Save();
            }
            catch (Exception) 
            {
                //log
                return RedirectToAction("PaymentFailed");            
            }

            return RedirectToAction("PaymentSuccessful");
        }

        public IActionResult PaymentSuccessful() => View("PaymentSuccessful");
        public IActionResult PaymentFailed() => View("PaymentFailed");

        public IActionResult PaymentSuccessfulUsingCreditOrDebitCard()
        {
            var targetOrderToPayFor = _unitOfWork
                .OrderRepository
                .Get(x => x.CustomerID == CurrentCustomer.UserID && x.StatusID == (int)eOrderStatuses.SHIPPED);

            ArgumentNullException.ThrowIfNull(nameof(targetOrderToPayFor));

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

                _unitOfWork.Save();
            }
            catch (Exception)
            {
                //log
                return RedirectToAction("PaymentFailed");
            }

            return RedirectToAction("PaymentSuccessful");
        }
    }
}
