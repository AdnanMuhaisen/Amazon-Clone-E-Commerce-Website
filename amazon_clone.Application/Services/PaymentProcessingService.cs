using amazon_clone.Application.Interfaces;
using amazon_clone.Application.Managers;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.Exceptions;

namespace amazon_clone.Application.Services
{
    public class PaymentProcessingService : IPaymentProcessingService
    {
        public IUnitOfWork _unitOfWork { get; }
        public IProductQuantityManager ProductQuantityManager { get; }

        public PaymentProcessingService(IUnitOfWork unitOfWork, IProductQuantityManager productQuantityManager)
        {
            _unitOfWork = unitOfWork;
            ProductQuantityManager = productQuantityManager;
        }

        public void PayByCashOnDelivery(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            var cartProductsQuantities = _unitOfWork
                .ShoppingCartProductRepository
                .GetAllAsNoTracking(x => x.ShoppingCartID == order.ShoppingCartID)?
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
                TotalAmount = order!.Total,
                CustomerID = CurrentCustomer.UserID!,
                OrderID = order.OrderID
            };
            try
            {
                order.StatusID = (int)eOrderStatuses.DELIVERED;

                _unitOfWork.PaymentRepository.Add(payment);
                _unitOfWork.OrderRepository.Update(order);

                ProductQuantityManager.DecreaseTheProductsQuantityInAShoppingCart(order.ShoppingCart, cartProductsQuantities);

                _unitOfWork.Save();
            }
            catch (Exception)
            {
                //log
                throw new InvalidPaymentOperationException($"can not save the payment data");
            }
        }

        public void PayByCreditOrDebitCard(Order targetOrderToPayFor)
        {
            ArgumentNullException.ThrowIfNull((targetOrderToPayFor));

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

                ProductQuantityManager.DecreaseTheProductsQuantityInAShoppingCart(targetOrderToPayFor.ShoppingCart, cartProductsQuantities);

                _unitOfWork.Save();
            }
            catch (Exception)
            {
                //log
                throw new InvalidPaymentOperationException($"can not save the payment data");
            }
        }


    }
}
