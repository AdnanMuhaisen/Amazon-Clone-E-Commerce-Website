using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace amazon_clone.web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var customerOrder = _unitOfWork
                .OrderRepository
                .GetAsNoTracking(filter: x => x.CustomerID == CurrentCustomer.UserID
                && x.StatusID == (int)eOrderStatuses.PROCESSING,
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts)
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartPromoCode!));

            if(customerOrder is null)
            {
                // we need to create a new order with the processing state
                ShoppingCart shoppingCart = new ShoppingCart
                {
                    PromoCodeID = null
                };
                _unitOfWork.ShoppingCartRepository.Add(shoppingCart);
                _unitOfWork.Save();

                int targetShoppingCartID = _unitOfWork
                    .ShoppingCartRepository
                    .GetAllAsNoTracking()!
                    .Select(x => x.ShoppingCartID)
                    .OrderBy(x => x)
                    .LastOrDefault();

                ArgumentNullException.ThrowIfNull(nameof(targetShoppingCartID));

                customerOrder = new Order
                {
                    OrderDateTime = DateTime.Now,
                    CustomerID = CurrentCustomer.UserID!,
                    StatusID = (int)eOrderStatuses.PROCESSING,
                    ShoppingCartID = targetShoppingCartID
                };
                _unitOfWork.OrderRepository.Add(customerOrder);
                _unitOfWork.Save();
            }

            ArgumentNullException.ThrowIfNull(nameof(customerOrder));

            return View(customerOrder.ShoppingCart);
        }
    }
}
