using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Application.Services
{
    public class WishListService : IWishListService
    {
        public IAppUnitOfWork _unitOfWork { get; }

        public WishListService(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateACustomerWishList()
        {
            var wishList = new WishList()
            {
                Products = new List<CustomerProduct>()
            };

            _unitOfWork.WishListRepository.Add(wishList);

            _unitOfWork.Save();

            var currentCustomerData = _unitOfWork
            .UsersRepository
            .Get(x => x.Id == CurrentCustomer.UserID);

            var targetWishlistID = _unitOfWork
                .WishListRepository
                .GetAllAsNoTracking()?
                .Select(x => x.WishListID)
                .MaxBy(x => x);

            ArgumentNullException.ThrowIfNull(nameof(targetWishlistID));
            currentCustomerData!.WishListID = targetWishlistID;
            CurrentCustomer.WishlistID = targetWishlistID;

            _unitOfWork.Save();
        }

        public void AddProductToWishList(int ProductID)
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

            if (customerWishlist is null)
            {
                CreateACustomerWishList();

                customerWishlist = _unitOfWork
                    .WishListRepository
                    .GetAllAsNoTracking(include: i => i.Include(x => x.Products!))?
                    .MaxBy(x => x.WishListID);

                ArgumentNullException.ThrowIfNull(customerWishlist);
            }

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
        }

        public void RemoveProductFromWishlist(int ProductID)
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
        }
    }
}
