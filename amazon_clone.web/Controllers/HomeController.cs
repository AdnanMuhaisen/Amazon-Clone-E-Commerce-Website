
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.View_Models;
using amazon_clone.Utility.App_Details;
using Microsoft.AspNetCore.Mvc;

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
            var allProducts = _unitOfWork
                .ProductRepository
                .GetAllAsNoTracking(IncludeProperties: "Category")?
                .Select(p => new Product
                {
                    ProductID = p.ProductID,
                    Name = (p.Name.Length > 25) ? p.Name.Substring(0, 25) + "..." : p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    // if there is no image for the product set the default product image. 
                    ImageUrl = @"/images/products/" + p.ImageUrl ?? StaticDetails.DEFAULT_PRODUCT_IMAGE,
                    CategoryID = p.CategoryID,
                    Category = p.Category
                });

            //if the list is of length 0 or null
            ArgumentNullException.ThrowIfNull(nameof(allProducts));

            if (!(allProducts!.Any()))
            {
                return View(new HomeViewModel
                {
                    allProducts = Enumerable.Empty<CustomerProduct>(),
                    clothesProducts = Enumerable.Empty<ClothingProduct>()
                });
            }

            var customerProductsData = _unitOfWork.CustomerProductRepository
                .GetAllAsNoTracking()?
                .Select(x => new
                {
                    ProductID = x.ProductID,
                    CustomerProductID = x.CustomerProductID
                });

            //if the list is of length 0 or null
            ArgumentNullException.ThrowIfNull(nameof(customerProductsData));

            if (!(customerProductsData!.Any()))
            {
                return View(new HomeViewModel
                {
                    allProducts = Enumerable.Empty<CustomerProduct>(),
                    clothesProducts = Enumerable.Empty<ClothingProduct>()
                });
            }


            var customerProducts = allProducts!
                .Join(customerProductsData!,
                o => o.ProductID,
                i => i.ProductID,
                (o, i) => new CustomerProduct
                {
                    ProductID = o.ProductID,
                    Name = o.Name,
                    Description = o.Description,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    ImageUrl = o.ImageUrl,
                    CustomerProductID = i.CustomerProductID,
                    Category= o.Category,
                });

            var clothesProductsData = _unitOfWork
                .ClothesProductRepository
                .GetAllAsNoTracking(IncludeProperties: "TargetGender")?
                .Select(x => new
                {
                    ClothesProductID = x.ClothingProductID,
                    CustomerProductID = x.CustomerProductID,
                    TargetGenderID = x.TargetGenderID
                });

            ArgumentNullException.ThrowIfNull(nameof(clothesProductsData));

            if (!(clothesProductsData!.Any()))
            {
                return View(new HomeViewModel
                {
                    allProducts = customerProducts,
                    clothesProducts = Enumerable.Empty<ClothingProduct>()
                });
            }

            var clothesProducts = customerProducts
                .Join(clothesProductsData!,
                o => o.CustomerProductID,
               i => i.CustomerProductID,
               (o, i) => new ClothingProduct
               {
                   ProductID = o.ProductID,
                   Name = o.Name,
                   Description = o.Description,
                   Price = o.Price,
                   Quantity = o.Quantity,
                   ImageUrl = o.ImageUrl,
                   CustomerProductID = o.CustomerProductID,
                   ClothingProductID = i.ClothesProductID,
                   TargetGenderID = i.TargetGenderID,
                   Category = o.Category,
               });

            var indexViewModel = new HomeViewModel();
            indexViewModel.allProducts = customerProducts!;
            indexViewModel.clothesProducts = clothesProducts;

            return View(indexViewModel);
        }




    }
}
