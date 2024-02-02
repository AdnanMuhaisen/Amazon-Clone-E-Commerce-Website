using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Domain.View_Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace amazon_clone.Dashboard.Controllers
{
    public class CustomerProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICustomerProductService customerProductService;

        public CustomerProductController(IUnitOfWork unitOfWork,ICustomerProductService customerProductService)
        {
            this.unitOfWork = unitOfWork;
            this.customerProductService = customerProductService;
        }

        public IActionResult Index()
        {
            var customerProducts = customerProductService.GetAllCustomerProductsWithAllDetails();

            return View(customerProducts);
        }

        [HttpGet]
        public IActionResult UpsertCustomerProduct(int? ProductID = default)
        {
            CustomerProductViewModel customerProductViewModel = customerProductService.GetCustomerProductForUpsert(ProductID);

            return View(customerProductViewModel);
        }
        
        [HttpPost]
        public IActionResult UpsertCustomerProduct(CustomerProduct customerProduct,IFormFile formFile)
        {
            // we need to add or update the images in the project folder also 

            ModelState["customerProduct.ProductID"]!.ValidationState = ModelValidationState.Valid;
            
            if(formFile is null && customerProduct.ProductID != 0)
            {
                // in this case the user does not update the image of the product and we need to save the product image
                customerProduct.ImageUrl = unitOfWork
                    .CustomerProductRepository
                    .GetAllAsNoTracking(filter: x => x.ProductID == customerProduct.ProductID)?
                    .Select(x => x.ImageUrl)
                    .First();

                ModelState["formFile"]!.ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {
                customerProductService.UpsertCustomerProduct(customerProduct, formFile!);
            }
            else
            {
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveCustomerProduct(int ProductID) 
        {
            if(ProductID == 0)
            {
                return NotFound();
            }

            customerProductService.RemoveCustomerProduct(ProductID);

            return RedirectToAction("Index");
        }
    }
}
