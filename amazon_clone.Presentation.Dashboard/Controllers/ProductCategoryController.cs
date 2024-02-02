using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace amazon_clone.Dashboard.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductCategoryManager productCategoryManager;

        public ProductCategoryController(IUnitOfWork unitOfWork,IProductCategoryManager productCategoryManager)
        {
            this.unitOfWork = unitOfWork;
            this.productCategoryManager = productCategoryManager;
        }

        public IActionResult Index()
        {
            var productCategories = unitOfWork
                .ProductCategoryRepository
                .GetAllAsNoTracking()?
                .ToList();

            ArgumentNullException.ThrowIfNull(productCategories);

            return View(productCategories);
        }

        [HttpGet]
        public IActionResult UpsertProductCategory(int CategoryID = 0)
        {
            if(CategoryID == 0)
            {
                //insert 
                return View();
            }
            else
            {
                //update
                var targetCategory = unitOfWork
                    .ProductCategoryRepository
                    .Get(filter: x => x.CategoryID == CategoryID);

                ArgumentNullException.ThrowIfNull(targetCategory);

                return View(targetCategory);
            }
        }

        [HttpPost]
        public IActionResult UpsertProductCategory(ProductCategory productCategory)
        {
            ModelState["CategoryID"]!.ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                productCategoryManager.UpsertProductCategory(productCategory);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveProductCategory(int CategoryID)
        {
            productCategoryManager.RemoveProductCategory(CategoryID);

            return RedirectToAction("Index");
        }
    }
}
