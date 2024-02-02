using amazon_clone.Application.Interfaces;
using amazon_clone.Presentation.CustomerApp.Filters;
using Microsoft.AspNetCore.Mvc;



namespace amazon_clone.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerProductService customerProductService;
        private readonly IClothingCustomerProductService clothingCustomerProductService;


        public HomeController(
            ILogger<HomeController> logger,
            ICustomerProductService customerProductService,
            IClothingCustomerProductService clothingCustomerProductService
            )
        {
            _logger = logger;
            this.customerProductService = customerProductService;
            this.clothingCustomerProductService = clothingCustomerProductService;
        }

        
        public IActionResult Index()
        {
            var customerProducts = customerProductService.GetAllCustomerProducts();

            return View(customerProducts);
        }

        [HttpGet]
        public IActionResult Search(string valueToSearch)
        {
            var searchResult = customerProductService.GetSearchResult(valueToSearch);

            return View("Index", searchResult);
        }

        public IActionResult ElectronicProducts()
        {
            var electronicProducts = customerProductService.GetAllElectronicProducts();

            return View(electronicProducts);
        }

        public IActionResult JewelryProducts()
        {
            var jewelryCustomerProducts = customerProductService.GetAllJewelryProducts();

            return View(jewelryCustomerProducts);
        }


        public IActionResult MenClothingProduct()
        {
            var menClothingProducts = clothingCustomerProductService.GetAllMenClothingProducts();

            return View(menClothingProducts);
        }

        public IActionResult WomenClothingProducts()
        {
            var womenClothingProducts = clothingCustomerProductService.GetAllWomenClothingProducts();

            return View(womenClothingProducts);
        }
    }
}
