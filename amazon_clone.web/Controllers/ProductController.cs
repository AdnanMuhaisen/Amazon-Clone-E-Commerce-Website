using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users.CurrentUsers;
using amazon_clone.Models.View_Models;
using amazon_clone.Utility.App_Details;
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

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            var customerProductData = _unitOfWork
                .ProductRepository
                .GetAsNoTracking(filter: x => x.ProductID == customerProductInfo.ProductID);

            if(customerProductData is null)
            {
                return NotFound();
            }
            customerProductData!.ImageUrl = @"/images/products/" + customerProductData.ImageUrl;

            // check if the product in the customer Wishlist
            SetProductAdditionStatusForWishlistAndShoppingCart(customerProductData.ProductID);

            return View(customerProductData);
        }

        public IActionResult ClothingCustomerProduct(ClothingProductDto clothingProductInfo)
        {
            var clothingProduct = _unitOfWork
            .ClothesProductRepository
            .GetAllAsNoTracking(x => x.ProductID == clothingProductInfo.ProductID && x.ClothingProductID == clothingProductInfo.ClothingProductID,
            IncludeProperties: "Sizes")?
            .Select(p => new ClothingProduct
            {
                ProductID = p.ProductID,
                Name = (p.Name.Length > 25) ? p.Name.Substring(0, 25) + "..." : p.Name,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                // if there is no image for the product set the default product image. 
                ImageUrl = @"/images/products/" + p.ImageUrl ?? StaticDetails.DEFAULT_PRODUCT_IMAGE,
                CustomerProductID = p.CustomerProductID,
                ClothingProductID = p.ClothingProductID,
                Sizes = p.Sizes
            })
            .FirstOrDefault();

            ArgumentNullException.ThrowIfNull((clothingProduct));

            var ProductSizes = clothingProduct!
                .Sizes
                .Select(x => new SelectListItem()
                {
                    Text = x.Size,
                    Value = x.Size
                });

            // check if the product in the customer Wishlist
            SetProductAdditionStatusForWishlistAndShoppingCart(clothingProductInfo.ProductID);

            var viewModel = new ClothingCustomerProductViewModel(clothingProduct, ProductSizes);
            return View(viewModel);
        }
    }
}
