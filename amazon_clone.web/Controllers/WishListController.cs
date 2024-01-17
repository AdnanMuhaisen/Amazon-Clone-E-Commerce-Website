using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.web.Controllers
{
    public class WishListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WishListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                wishList = new WishList()
                {
                    Products = new List<CustomerProduct>()
                };

                _unitOfWork.WishListRepository.Add(wishList);

                _unitOfWork.Save();

                var currentCustomer = _unitOfWork
                .UsersRepository
                .Get(x => x.Id == CurrentCustomer.UserID);

                var targetListID = _unitOfWork
                    .WishListRepository
                    .GetAllAsNoTracking()?
                    .Select(x => x.WishListID)
                    .OrderBy(x => x)
                    .LastOrDefault();

                ArgumentNullException.ThrowIfNull(nameof(targetListID));
                currentCustomer!.WishListID = targetListID;
                _unitOfWork.Save();
            }

            foreach (var product in wishList.Products!)
            {
                product.ImageUrl = @"/images/products/" + product.ImageUrl;
            }

            return View(wishList);
        }

        public IActionResult AddProductToWishlist(int ProductID)
        {
            var product = _unitOfWork.ProductRepository
                .GetAsNoTracking(x => x.ProductID == ProductID);

            var customerProductID = _unitOfWork
                .CustomerProductRepository
                .GetAllAsNoTracking(x => x.ProductID == ProductID)?
                .Select(x => x.CustomerProductID)
                .First();

            ArgumentNullException.ThrowIfNull(customerProductID);

            var customerWishlist = _unitOfWork
                .WishListRepository
                .GetAsNoTracking(filter: x => x.WishListID == CurrentCustomer.WishlistID, "Products");

            ArgumentNullException.ThrowIfNull(customerWishlist);

            ArgumentNullException.ThrowIfNull(customerWishlist.Products);

            if (customerWishlist.Products.Any(x => x.ProductID == ProductID))
            {
                // do nothing
            }
            else
            {
                _unitOfWork.WishListProductRepository.Add(new WishListProduct()
                {
                    ProductID = ProductID,
                    ListID = customerWishlist.WishListID
                });
                _unitOfWork.Save();
            }

            return RedirectToAction("Index", routeValues: CurrentCustomer.UserID);
        }

        public IActionResult RemoveProductFromWishlist(int ProductID)
        {
           //the current customer static class is already filled, so we can get the customer ID from this static class
           var customerWishlistID = _unitOfWork
               .WishListRepository
               .GetAllAsNoTracking(filter: x => x.WishListID == CurrentCustomer.WishlistID)?
                .Select(x => x.WishListID)
                .First();

        ArgumentNullException.ThrowIfNull(customerWishlistID);

            var targetRecordToDelete = _unitOfWork
                .WishListProductRepository
                .Get(filter: x => x.ProductID == ProductID && x.ListID == customerWishlistID);

        ArgumentNullException.ThrowIfNull(nameof(targetRecordToDelete));

            _unitOfWork
                .WishListProductRepository
                .Remove(targetRecordToDelete!);

            _unitOfWork.Save();

            return RedirectToAction("Index", routeValues: CurrentCustomer.UserID);
        }
    }
}
