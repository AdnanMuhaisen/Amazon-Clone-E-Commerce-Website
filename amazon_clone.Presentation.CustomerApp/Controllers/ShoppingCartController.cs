using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Domain.View_Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Presentation.CustomerApp.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace amazon_clone.web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IOrderProcessingService orderProcessingService;

        public ShoppingCartController(
            IUnitOfWork unitOfWork,
            IShoppingCartService shoppingCartService,
            IOrderProcessingService orderProcessingService
            )
        {
            _unitOfWork = unitOfWork;
            this.shoppingCartService = shoppingCartService;
            this.orderProcessingService = orderProcessingService;
        }

        public IActionResult Index()
        {
            var customerOrder = _unitOfWork
                .OrderRepository
                .GetAsNoTracking(filter: x => x.CustomerID == CurrentCustomer.UserID
                && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts)
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartPromoCode!));

            if(customerOrder is null)
            {
                // we need to create a new order with the processing state
                orderProcessingService.CreateANewOrderWithShoppingCart();

                //now get the created order with shopping cart
                customerOrder = _unitOfWork
                .OrderRepository
                .GetAsNoTracking(filter: x => x.CustomerID == CurrentCustomer.UserID
                && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                include: i => i
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartProducts)
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.CartPromoCode!));
            }

            ArgumentNullException.ThrowIfNull(customerOrder);

            return View(new ShoppingCartViewModel(customerOrder, string.Empty));
        }

        public IActionResult RemoveProductFromShoppingCart(int CustomerProductID)
        {
            shoppingCartService.RemoveProductFromShoppingCart(CustomerProductID);

            return RedirectToAction("Index");
        }

        public IActionResult AddProductToShoppingCart(int ProductID)
        {
            shoppingCartService.AddProductToShoppingCart(ProductID);

            return RedirectToAction("Index");
        }
    }
}
