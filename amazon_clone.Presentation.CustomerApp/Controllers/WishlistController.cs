using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.web.Controllers
{
    public class WishListController : Controller
    {
        private readonly IAppUnitOfWork _unitOfWork;
        private readonly IWishListService wishListService;

        public WishListController(IAppUnitOfWork unitOfWork,IWishListService wishListService)
        {
            _unitOfWork = unitOfWork;
            this.wishListService = wishListService;
        }

        public IActionResult Index(string UserID)
        {
            //possibly null reference exception
            var wishList = _unitOfWork
                 .WishListRepository
                 .GetAsNoTracking(filter: x => x.WishListID == CurrentCustomer.WishlistID,
                 include: i => i.Include(x => x.Products!)
                 .ThenInclude(y => y.Category));

            if (wishList is null)
            {
                wishListService.CreateACustomerWishList();

                wishList = _unitOfWork
                    .WishListRepository
                    .GetAllAsNoTracking(filter: x => x.WishListID == CurrentCustomer.WishlistID, include: i => i.Include(x => x.Products!))?
                    .MaxBy(x => x.WishListID);
            }

            foreach (var product in wishList!.Products!)
            {
                product.ImageUrl = @"/images/products/" + product.ImageUrl;
            }

            return View(wishList);
        }

        public IActionResult AddProductToWishlist(int ProductID)
        {
            wishListService.AddProductToWishList(ProductID);

            return RedirectToAction("Index", routeValues: CurrentCustomer.UserID);
        }

        public IActionResult RemoveProductFromWishlist(int ProductID)
        {
            //the current customer static class is already filled, so we can get the customer ID from this static class
            wishListService.RemoveProductFromWishlist(ProductID);

            return RedirectToAction("Index", routeValues: CurrentCustomer.UserID);
        }
    }
}
