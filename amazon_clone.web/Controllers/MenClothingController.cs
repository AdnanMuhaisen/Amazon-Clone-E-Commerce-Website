using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Utility.App_Details;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var menClothingProducts = _unitOfWork
                .ClothesProductRepository
                .GetAllAsNoTracking(filter: x => x.TargetGenderID == (int)eGender.Male, include: i => i.Include(x => x.Category).Include(x => x.TargetGender))?
                .Select(x => new ClothingProduct
                {
                    ProductID = x.ProductID,
                    Name = (x.Name.Length > 25) ? x.Name.Substring(0, 25) + "..." : x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    // if there is no image for the product set the default product image. 
                    ImageUrl = @"/images/products/" + x.ImageUrl ?? StaticDetails.DEFAULT_PRODUCT_IMAGE,
                    CategoryID = x.CategoryID,
                    Category = x.Category,
                    CustomerProductID = x.CustomerProductID,
                    ClothingProductID = x.ClothingProductID,
                    TargetGender = x.TargetGender
                })
                .ToList();

            return View(menClothingProducts);
        }
    }
}
