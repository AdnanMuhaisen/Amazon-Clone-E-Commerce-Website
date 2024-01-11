using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Utility.App_Details;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.web.Controllers
{
    public class MenClothingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenClothingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var menClothingCategory = _unitOfWork
                .ProductCategoryRepository
                .GetAsNoTracking(filter: c => c.CategoryID == (int)eProductCategories.Mens_Clothing);

            var customerProducts = _unitOfWork
               .ProductRepository
               .GetAllAsNoTracking(filter: p => p.CategoryID == menClothingCategory!.CategoryID)?
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
                   Category = menClothingCategory
               })
             .Join(_unitOfWork
                .CustomerProductRepository
                .GetAllAsNoTracking()?
                .Select(x => new
                {
                    ProductID = x.ProductID,
                    CustomerProductID = x.CustomerProductID
                })!
                , o => o.ProductID,
                i => i.ProductID,
                (o, i) => new CustomerProduct()
                {
                    ProductID = o.ProductID,
                    Name = o.Name,
                    Description = o.Description,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    // if there is no image for the product set the default product image. 
                    ImageUrl = o.ImageUrl,
                    CategoryID = o.CategoryID,
                    Category = menClothingCategory!,
                    CustomerProductID = i.CustomerProductID
                });

            var menClothingProducts = customerProducts!
                .Join(_unitOfWork
                .ClothesProductRepository
                .GetAllAsNoTracking(IncludeProperties: "TargetGender")!
                .Select(x => new
                {
                    CustomerProductID = x.CustomerProductID,
                    ClothingProductID = x.ClothingProductID,
                    TargetGender = x.TargetGender
                }),
                o => o.CustomerProductID,
                i => i.CustomerProductID,
                (o, i) => new ClothingProduct()
                {
                    ProductID = o.ProductID,
                    Name = o.Name,
                    Description = o.Description,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    // if there is no image for the product set the default product image. 
                    ImageUrl = o.ImageUrl,
                    CategoryID = o.CategoryID,
                    Category = menClothingCategory!,
                    CustomerProductID = i.CustomerProductID,
                    ClothingProductID = i.ClothingProductID,
                    TargetGender = i.TargetGender
                })
                .ToList();

            return View(menClothingProducts);
        }
    }
}
