using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Domain.View_Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IPaymentProcessingService paymentProcessingService;
        private readonly IOrderProcessingService orderProcessingService;
        //private readonly IEmailSender _emailSender;

        public PaymentController(
            IAppUnitOfWork unitOfWork,
            IPaymentProcessingService paymentProcessingService,
            IOrderProcessingService orderProcessingService
            //IEmailSender emailSender
            )
        {
            _unitOfWork = unitOfWork;
            this.paymentProcessingService = paymentProcessingService;
            this.orderProcessingService = orderProcessingService;
            //_emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var targetOrder = orderProcessingService.GetOrder(x =>
            (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING)
                && x.CustomerID == CurrentCustomer.UserID);

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

            try
            {
                paymentProcessingService.PayByCashOnDelivery(targetOrderToPayFor);
            }
            // do not change the exception
            catch (InvalidPaymentOperationException) 
            {
                //log
                return RedirectToAction("PaymentFailed");            
            }

            return RedirectToAction("PaymentSuccessful", routeValues: targetOrderToPayFor.OrderID);
        }

        public IActionResult PaymentSuccessful(int OrderID)
        {
            //_emailSender.SendEmailAsync(
            //    CurrentCustomer.Email,
            //    "Amazon Clone Order Notifier",
            //    $"You have successfully paid for the order with id {OrderID}"
            //    );


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

            
            try
            {
                paymentProcessingService.PayByCreditOrDebitCard(targetOrderToPayFor);
            }
            catch (InvalidPaymentOperationException)
            {
                //log
                return RedirectToAction("PaymentFailed");
            }

            return RedirectToAction("PaymentSuccessful", routeValues: targetOrderToPayFor.OrderID);
        }
    }
}
