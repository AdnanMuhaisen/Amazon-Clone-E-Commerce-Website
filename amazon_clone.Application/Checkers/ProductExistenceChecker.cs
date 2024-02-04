using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Application.Checkers
{
    public class ProductExistenceChecker : IProductExistenceChecker
    {
        public IAppUnitOfWork _unitOfWork { get; }

        public ProductExistenceChecker(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public bool IsProductInTheCustomerWishlist(int ProductID)
        {
            // check if the product in the customer Wishlist
            var customerWishlist = _unitOfWork
                .WishListRepository
                .GetAsNoTracking(x => x.WishListID == CurrentCustomer.WishlistID, IncludeProperties: "Products");

            if (customerWishlist is null)
            {
                return false;
            }
            else if (customerWishlist!.Products!.Any(x => x.ProductID == ProductID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsProductInTheCustomerShoppingCart(int ProductID)
        {
            // check if the product in the customer shopping cart 
            var targetOrderContainsTheCart = _unitOfWork
                 .OrderRepository
                 .Get(filter: x => x.CustomerID == CurrentCustomer.UserID 
                 && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                 include: i => i
                 .Include(x => x.ShoppingCart)
                 .ThenInclude(x => x.CartProducts));


            if (targetOrderContainsTheCart is null)
            {
                return false;
            }
            else if (targetOrderContainsTheCart!.ShoppingCart.CartProducts.Any(x => x.ProductID == ProductID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
