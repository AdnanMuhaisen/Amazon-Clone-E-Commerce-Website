using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users;
using amazon_clone.Utility.App_Details;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        
        public IActionResult Index()
        {
            if(CurrentCustomer.UserID is null)
            {
                // this implementation is temporary 
                // instead of this condition , create a custom attribute to solve this problem
                return NotFound();
            }

            var customerProducts = _unitOfWork.CustomerProductRepository
                .GetAllAsNoTracking(include: i => i.Include(x => x.Category))?
                .Select(x => new CustomerProduct
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
                    CustomerProductID = x.CustomerProductID
                })
                .ToList();

            //if the list is of length 0 or null
            ArgumentNullException.ThrowIfNull(nameof(customerProducts));

            return View(customerProducts);
        }

        [HttpGet]
        public IActionResult Search(string valueToSearch)
        {
            var customerProducts = _unitOfWork
                .CustomerProductRepository
                .GetAllAsNoTracking(include: i => i.Include(x => x.Category))?
                .Select(p => new CustomerProduct
                {
                    ProductID = p.ProductID,
                    Name = (p.Name.Length > 25) ? p.Name.Substring(0, 25) + "..." : p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    // if there is no image for the product set the default product image. 
                    ImageUrl = @"/images/products/" + p.ImageUrl ?? StaticDetails.DEFAULT_PRODUCT_IMAGE,
                    CategoryID = p.CategoryID,
                    Category = p.Category,
                    CustomerProductID = p.CustomerProductID
                })
                .ToList();

            //if the list is of length 0 or null
            ArgumentNullException.ThrowIfNull(nameof(customerProducts));

            var searchResult = customerProducts!.Where(x => x.Name.Contains(valueToSearch)).ToList();

            return View("Index", searchResult);
        }

        public IActionResult ElectronicProducts()
        {
            var customerProducts = _unitOfWork
                .CustomerProductRepository
                .GetAllAsNoTracking(filter: x => x.CategoryID == (int)eProductCategories.Electronics,
                include: i => i.Include(x => x.Category))?
                .Select(p => new CustomerProduct
                {
                    ProductID = p.ProductID,
                    Name = (p.Name.Length > 25) ? p.Name.Substring(0, 25) + "..." : p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    // if there is no image for the product set the default product image. 
                    ImageUrl = @"/images/products/" + p.ImageUrl ?? StaticDetails.DEFAULT_PRODUCT_IMAGE,
                    CategoryID = p.CategoryID,
                    Category = p.Category,
                    CustomerProductID = p.CustomerProductID
                })
                .ToList();

            ArgumentNullException.ThrowIfNull(nameof(customerProducts));

            return View(customerProducts);
        }

        public IActionResult JewelryProducts()
        {
            var jewelryCustomerProducts = _unitOfWork
                .CustomerProductRepository
                .GetAllAsNoTracking(filter: x => x.CategoryID == (int)eProductCategories.Jewelery, include: i => i.Include(x => x.Category))?
                .Select(p => new CustomerProduct
                {
                    ProductID = p.ProductID,
                    Name = (p.Name.Length > 25) ? p.Name.Substring(0, 25) + "..." : p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    // if there is no image for the product set the default product image. 
                    ImageUrl = @"/images/products/" + p.ImageUrl ?? StaticDetails.DEFAULT_PRODUCT_IMAGE,
                    CategoryID = p.CategoryID,
                    Category = p.Category,
                    CustomerProductID = p.CustomerProductID
                })
                .ToList();

            if (jewelryCustomerProducts is null)
            {
                jewelryCustomerProducts = new List<CustomerProduct>();
            }

            return View(jewelryCustomerProducts);
        }


        public IActionResult MenClothingProduct()
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

        public IActionResult WomenClothingProducts()
        {
            var womenClothingProducts = _unitOfWork
                .ClothesProductRepository
                .GetAllAsNoTracking(filter: x => x.TargetGenderID == (int)eGender.Female, include: i => i.Include(x => x.Category).Include(x => x.TargetGender))?
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

            return View(womenClothingProducts);
        }
    }
}
