using amazon_clone.DataAccess.Enums;
using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Utility.App_Details;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            if(jewelryCustomerProducts is null)
            {
                jewelryCustomerProducts = new List<CustomerProduct>();
            }

            return View(jewelryCustomerProducts);
        }
    }
}
