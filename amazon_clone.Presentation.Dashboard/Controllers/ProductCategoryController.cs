using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace amazon_clone.Dashboard.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        //private readonly IAdministratorTransactionController administratorTransactionController;

        public ProductCategoryController(
            IUnitOfWork unitOfWork
            //IAdministratorTransactionController administratorTransactionController
            )
        {
            this.unitOfWork = unitOfWork;
            //this.administratorTransactionController = administratorTransactionController;
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
                if(productCategory.CategoryID == 0)
                {
                    //insert
                    try
                    {
                        unitOfWork.ProductCategoryRepository.Add(productCategory);

                        //administratorTransactionController.AddNewAdministratorTransaction(
                        //    unitOfWork,
                        //    CurrentAdministrator.UserID!,
                        //    $"The product category : Category Name : {productCategory.CategoryName} is added");

                        unitOfWork.Save();
                    }
                    catch (Exception) 
                    {
                        //administratorTransactionController.AddNewAdministratorTransaction(
                        //    unitOfWork,
                        //    CurrentAdministrator.UserID!,
                        //    $"Try to add the product category : Category Name : {productCategory.CategoryName}");

                        unitOfWork.Save();
                    }
                }
                else
                {
                    //update
                    try
                    {
                        unitOfWork.ProductCategoryRepository.Update(productCategory);

                        //administratorTransactionController.AddNewAdministratorTransaction(
                        //    unitOfWork,
                        //    CurrentAdministrator.UserID!,
                        //    $"Update the product category : Category Name : {productCategory.CategoryName}");

                        unitOfWork.Save();
                    }
                    catch(Exception)
                    {
                        //administratorTransactionController.AddNewAdministratorTransaction(
                        //    unitOfWork,
                        //    CurrentAdministrator.UserID!,
                        //    $"Try to update the product category : Category Name : {productCategory.CategoryName}");

                        unitOfWork.Save();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveProductCategory(int CategoryID)
        {
            var targetCategoryToDelete = unitOfWork
                .ProductCategoryRepository
                .Get(x => x.CategoryID == CategoryID);

            ArgumentNullException.ThrowIfNull(targetCategoryToDelete);

            try
            {
                unitOfWork.ProductCategoryRepository.Remove(targetCategoryToDelete);

                //administratorTransactionController.AddNewAdministratorTransaction(
                //    unitOfWork,
                //    CurrentAdministrator.UserID!,
                //    $"Delete the product category : Category Name : {targetCategoryToDelete.CategoryName}");

                unitOfWork.Save();
            }
            catch(Exception)
            {
                //administratorTransactionController.AddNewAdministratorTransaction(
                //    unitOfWork,
                //    CurrentAdministrator.UserID!,
                //    $"Try to delete the product category : Category Name : {targetCategoryToDelete.CategoryName}");

                unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
