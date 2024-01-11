using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Utility.App_Details;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.web.Controllers
{
    public class ElectronicProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public ElectronicProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var electronicsCategory = _unitOfWork
                .ProductCategoryRepository
                .GetAsNoTracking(x => x.CategoryID == (int)eProductCategories.Electronics);
                
            ArgumentNullException.ThrowIfNull(nameof(electronicsCategory));

            var products = _unitOfWork
                .ProductRepository
                .GetAllAsNoTracking(filter: p => p.CategoryID == electronicsCategory!.CategoryID)?
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
                    Category = electronicsCategory
                });

            var customerProductsData = _unitOfWork
                .CustomerProductRepository
                .GetAllAsNoTracking()?
                .Select(x => new
            {
                ProductID = x.ProductID,
                CustomerProductID = x.CustomerProductID
            });

            ArgumentNullException.ThrowIfNull(nameof(customerProductsData));

            var electronics = products!
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

            return View(electronics);
        }





    }
}
