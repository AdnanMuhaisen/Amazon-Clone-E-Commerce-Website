using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Utility.App_Details;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.web.Controllers
{
    public class JewelryProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public JewelryProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var jewelryCategory = _unitOfWork
                .ProductCategoryRepository
                .Get(x => x.CategoryID == (int)eProductCategories.Jewelery);

            ArgumentNullException.ThrowIfNull(nameof(jewelryCategory));

            var products = _unitOfWork
                .ProductRepository
                .GetAll(filter: p => p.CategoryID == jewelryCategory!.CategoryID)?
                .Select(p => new
                {
                    ProductID = p.ProductID,
                    Name = (p.Name.Length > 25) ? p.Name.Substring(0, 25) + "..." : p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    // if there is no image for the product set the default product image. 
                    ImageUrl = @"/images/products/" + p.ImageUrl ?? StaticDetails.DEFAULT_PRODUCT_IMAGE,
                    CategoryID = p.CategoryID,
                    Category = jewelryCategory
                });

            var customerProductsData = _unitOfWork
                .CustomerProductRepository
                .GetAll()?
                .Select(x => new
                {
                    ProductID = x.ProductID,
                    CustomerProductID = x.CustomerProductID
                });

            ArgumentNullException.ThrowIfNull(nameof(customerProductsData));

            var jewelry = products!
                .Join(customerProductsData!,
                o => o.ProductID,
                i => i.ProductID,
                (o, i) => new CustomerProduct()
                {
                    ProductID = o.ProductID,
                    Name = o.Name,
                    Description = o.Description,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    ImageUrl = o.ImageUrl,
                    CategoryID = o.CategoryID,
                    Category = o.Category!,
                    CustomerProductID = i.CustomerProductID
                })
                .ToList();

            return View(jewelry);
        }
    }
}
