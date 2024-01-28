using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Domain.View_Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Web.WebPages.Html;

namespace amazon_clone.web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerProductService customerProductService;
        private readonly IProductExistenceChecker productExistenceChecker;
        private readonly IClothingCustomerProductService clothingCustomerProductService;

        public ProductController(
            IUnitOfWork unitOfWork,
            ICustomerProductService customerProductService,
            IProductExistenceChecker productExistenceChecker,
            IClothingCustomerProductService clothingCustomerProductService
            )
        {

            _unitOfWork = unitOfWork;
            this.customerProductService = customerProductService;
            this.productExistenceChecker = productExistenceChecker;
            this.clothingCustomerProductService = clothingCustomerProductService;
        }

        public IActionResult Index(int ProductID)
        {
            var customerProductsData = _unitOfWork
                .CustomerProductRepository
                .GetAllAsNoTracking()?
                .Select(x => new
                {
                    ProductID = x.ProductID,
                    CustomerProductID = x.CustomerProductID
                })
                .ToList();

            ArgumentNullException.ThrowIfNull(nameof(customerProductsData));
                
            var clothingProducts = _unitOfWork
                .ClothesProductRepository
                .GetAllAsNoTracking()?
                .Select(x => new
                {
                    ProductID= x.ProductID,
                    CustomerProductID = x.CustomerProductID,
                    ClothingProductID = x.ClothingProductID
                });

            ArgumentNullException.ThrowIfNull(nameof(clothingProducts));

            if (clothingProducts!.Where(x => x.ProductID == ProductID).Any())
            {
                var targetProduct = clothingProducts!.FirstOrDefault(x => x.ProductID == ProductID);
                return RedirectToAction("ClothingCustomerProduct", new ClothingProductDto
                {
                    ProductID = targetProduct!.ProductID,
                    CustomerProductID = targetProduct.CustomerProductID,
                    ClothingProductID = targetProduct.ClothingProductID
                });
            }
            else
            {
               return RedirectToAction("CustomerProduct", new CustomerProductDto
                {
                    ProductID = ProductID,
                    CustomerProductID = customerProductsData!
                    .First(x => x.ProductID == ProductID)
                    .CustomerProductID
                });
            }
        }

        public void SetProductAdditionStatusForWishlistAndShoppingCart(int ProductID)
        {
            // check if the product in the customer Wishlist

            var customerWishlist = _unitOfWork
                .WishListRepository
                .GetAsNoTracking(x => x.WishListID == CurrentCustomer.WishlistID, IncludeProperties: "Products");

            if(customerWishlist is null)
            {
                ViewData["Wishlist-Product-Status"] = "Add";
            }
            else if (customerWishlist!.Products!.Any(x => x.ProductID == ProductID))
            {
                ViewData["Wishlist-Product-Status"] = "Added";
            }
            else
            {
                ViewData["Wishlist-Product-Status"] = "Add";
            }

            // check if the product in the customer shopping cart 
            var targetOrderContainsTheCart = _unitOfWork
                 .OrderRepository
                 .Get(filter: x => x.CustomerID == CurrentCustomer.UserID && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING),
                 include: i => i
                 .Include(x => x.ShoppingCart)
                 .ThenInclude(x => x.CartProducts));


            if(targetOrderContainsTheCart is null) 
            {
                ViewData["ShoppingCart-Product-Status"] = "Add";
            }
            else if (targetOrderContainsTheCart!.ShoppingCart.CartProducts.Any(x => x.ProductID == ProductID))
            {
                ViewData["ShoppingCart-Product-Status"] = "Added";
            }
            else
            {
                ViewData["ShoppingCart-Product-Status"] = "Add";
            }
        }

        public IActionResult CustomerProduct(CustomerProductDto customerProductInfo)
        {
            ArgumentNullException.ThrowIfNull(nameof(customerProductInfo));

            var customerProductData = customerProductService.GetByID(customerProductInfo.ProductID);

            if (customerProductData is null)
            {
                return NotFound();
            }

            // check if the product in the customer Wishlist and shopping cart
            ViewData["Wishlist-Product-Status"] = productExistenceChecker.IsProductInTheCustomerWishlist(customerProductInfo.ProductID)
                ? "Added"
                : "Add";

            ViewData["ShoppingCart-Product-Status"] = productExistenceChecker.IsProductInTheCustomerShoppingCart(customerProductInfo.ProductID)
                ? "Added"
                : "Add";

            return View(customerProductData);
        }

        public IActionResult ClothingCustomerProduct(ClothingProductDto clothingProductInfo)
        {
            //service
            var clothingProduct = clothingCustomerProductService.Get(clothingProductInfo.ProductID, clothingProductInfo.ClothingProductID);

            ArgumentNullException.ThrowIfNull(clothingProduct);

            var ProductSizes = clothingProduct!
                .Sizes
                .Select(x => new SelectListItem()
                {
                    Text = x.Size,
                    Value = x.Size
                });

            // check if the product in the customer Wishlist and shopping cart
            ViewData["Wishlist-Product-Status"] = productExistenceChecker.IsProductInTheCustomerWishlist(clothingProductInfo.ProductID)
                ? "Added"
                : "Add";

            ViewData["ShoppingCart-Product-Status"] = productExistenceChecker.IsProductInTheCustomerShoppingCart(clothingProductInfo.ProductID)
                ? "Added"
                : "Add";

            var viewModel = new ClothingCustomerProductViewModel(clothingProduct, ProductSizes);
            return View(viewModel);
        }
    }
}
