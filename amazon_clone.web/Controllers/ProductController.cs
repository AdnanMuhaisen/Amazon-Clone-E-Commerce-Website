using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.View_Models;
using amazon_clone.Utility.App_Details;
using amazon_clone.Utility.DTOs;
using Microsoft.AspNetCore.Mvc;
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
            return View(customerProductData);
        }

        public IActionResult ClothingCustomerProduct(ClothingProductDto clothingProductInfo)
        {
            var clothingProductData = _unitOfWork
                .ProductRepository
                .GetAllAsNoTracking(x => x.ProductID == clothingProductInfo.ProductID)?
                .Select(p => new
                {
                    ProductID = p.ProductID,
                    Name = (p.Name.Length > 25) ? p.Name.Substring(0, 25) + "..." : p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    // if there is no image for the product set the default product image. 
                    ImageUrl = @"/images/products/" + p.ImageUrl ?? StaticDetails.DEFAULT_PRODUCT_IMAGE,
                })
                .Join(_unitOfWork
                .ClothesProductRepository
                .GetAllAsNoTracking(x => x.ClothingProductID == clothingProductInfo.ClothingProductID, IncludeProperties: "Sizes")?
                .Select(c => new
                {
                    CustomerProductID = c.CustomerProductID,
                    ClothingProductID = c.ClothingProductID,
                    Sizes = c.Sizes
                })!,
                o => clothingProductInfo.CustomerProductID,
                i => i.CustomerProductID,
                (o, i) => new ClothingProduct
                {
                    ProductID = o.ProductID,
                    Name = o.Name,
                    Description = o.Description,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    // if there is no image for the product set the default product image. 
                    ImageUrl = o.ImageUrl,
                    CustomerProductID = clothingProductInfo.CustomerProductID,
                    ClothingProductID = clothingProductInfo.ClothingProductID,
                    Sizes = i.Sizes
                })
                .FirstOrDefault();

            ArgumentNullException.ThrowIfNull(nameof(clothingProductData));

            var ProductSizes = clothingProductData!
                .Sizes
                .Select(x => new SelectListItem()
                {
                    Text = x.Size,
                    Value = x.Size
                });

            var viewModel = new ClothingCustomerProductViewModel(clothingProductData, ProductSizes);
            return View(viewModel);
        }


    }
}
