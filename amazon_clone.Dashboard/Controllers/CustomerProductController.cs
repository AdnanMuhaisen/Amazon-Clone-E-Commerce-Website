using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users.CurrentUsers;
using amazon_clone.Models.View_Models;
using amazon_clone.Utility.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace amazon_clone.Dashboard.Controllers
{
    public class CustomerProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CustomerProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var customerProducts = unitOfWork
                .CustomerProductRepository
                .GetAllAsNoTracking()?
                .Select(x => new CustomerProduct
                {
                    ProductID = x.ProductID,
                    CustomerProductID = x.CustomerProductID,
                    Name = (x.Name.Length > 20) ? x.Name.Substring(0, 20) + "..." : x.Name,
                    Description = (x.Description.Length > 20) ? x.Description.Substring(0, 20) + "..." : x.Description,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Quantity = x.Quantity,
                    IsDeleted = x.IsDeleted,
                    DeleteDate = x.DeleteDate,
                    ProductCreationDetails = x.ProductCreationDetails
                })
                .ToList();

            ArgumentNullException.ThrowIfNull(customerProducts);

            if (!customerProducts.Any())
            {
                customerProducts = new List<CustomerProduct>();
            }

            return View(customerProducts);
        }

        [HttpGet]
        public IActionResult UpsertCustomerProduct(int? ProductID = default)
        {
            var productCategories = unitOfWork
                .ProductCategoryRepository
                .GetAllAsNoTracking()?
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                });

            ArgumentNullException.ThrowIfNull(productCategories);

            if(!productCategories.Any())
            {
                productCategories = Enumerable.Empty<SelectListItem>();
            }

            CustomerProductViewModel customerProductViewModel;
            if (ProductID is not null)
            {
                var targetProductToUpdate = unitOfWork
                    .CustomerProductRepository
                    .Get(x => x.ProductID == ProductID);

                ArgumentNullException.ThrowIfNull(targetProductToUpdate);

                customerProductViewModel = new CustomerProductViewModel(productCategories, targetProductToUpdate);
            }
            else
            {
                customerProductViewModel = new CustomerProductViewModel(productCategories, default!);
            }

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
                if (formFile is not null)
                {
                    // because of when we need to decode this we need the actual name of the file to add it to the wwwroot folder
                    customerProduct.ImageUrl = formFile.FileName.ToBase64String();
                }

                if (customerProduct.ProductID == 0)
                {
                    // insert
                    try
                    {
                        int lastCustomerProductID = unitOfWork
                            .CustomerProductRepository
                            .GetAllAsNoTracking()!
                            .Select(x => x.CustomerProductID)
                            .MaxBy(x => x);

                        customerProduct.CustomerProductID = ++lastCustomerProductID;
                        unitOfWork.CustomerProductRepository.Add(customerProduct);

                        var AdminTransaction = new AdministratorTransaction
                        {
                            AdministratorID = CurrentAdministrator.UserID!,
                            TransactionDateTime = DateTime.Now,
                            TransactionLog = $"Add the product : product name is {customerProduct.Name}"
                        };

                        unitOfWork.CustomerProductRepository.Add(customerProduct);
                        unitOfWork.AdministratorsTransactionsRepository.Add(AdminTransaction);

                        unitOfWork.Save();

                        TempData["InsertNewProduct"] = "The Product Is Added";
                    }
                    catch (Exception)
                    {
                        var AdminTransaction = new AdministratorTransaction
                        {
                            AdministratorID = CurrentAdministrator.UserID!,
                            TransactionDateTime = DateTime.Now,
                            TransactionLog = $"Try to add the product : product name is {customerProduct.Name}"
                        };

                        unitOfWork.AdministratorsTransactionsRepository.Add(AdminTransaction);

                        unitOfWork.Save();

                        TempData["InsertNewProduct"] = "Error";
                    }
                }
                else
                {
                    //update
                    try
                    {
                        var targetCustomerProductToUpdate = unitOfWork
                            .CustomerProductRepository
                            .Get(x => x.ProductID == customerProduct.ProductID);

                        ArgumentNullException.ThrowIfNull(targetCustomerProductToUpdate);

                        //manual update
                        targetCustomerProductToUpdate.Name = customerProduct.Name;
                        targetCustomerProductToUpdate.Description = customerProduct.Description;
                        targetCustomerProductToUpdate.Price = customerProduct.Price;
                        targetCustomerProductToUpdate.Quantity = customerProduct.Quantity;
                        targetCustomerProductToUpdate.ImageUrl = customerProduct.ImageUrl;
                        targetCustomerProductToUpdate.CategoryID = customerProduct.CategoryID;

                        var AdminTransaction = new AdministratorTransaction
                        {
                            AdministratorID = CurrentAdministrator.UserID!,
                            TransactionDateTime = DateTime.Now,
                            TransactionLog = $"Update the product : product name is {customerProduct.Name}"
                        };

                        unitOfWork.AdministratorsTransactionsRepository.Add(AdminTransaction);

                        unitOfWork.Save();

                        TempData["UpdateCustomerProduct"] = "The product was updated";
                    }
                    catch(Exception)
                    {
                        var AdminTransaction = new AdministratorTransaction
                        {
                            AdministratorID = CurrentAdministrator.UserID!,
                            TransactionDateTime = DateTime.Now,
                            TransactionLog = $"Try to update the product : product name is {customerProduct.Name}"
                        };

                        unitOfWork.AdministratorsTransactionsRepository.Add(AdminTransaction);

                        unitOfWork.Save();

                        TempData["UpdateCustomerProduct"] = "Error";
                    }
                }
                
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

            var targetProductToDelete = unitOfWork
                .CustomerProductRepository
                .Get(filter: x => x.ProductID == ProductID);

            ArgumentNullException.ThrowIfNull(targetProductToDelete);

            try
            {
                unitOfWork.ProductRepository.Remove((Product)targetProductToDelete);

                var transaction = new AdministratorTransaction
                {
                    AdministratorID = CurrentAdministrator.UserID!,
                    TransactionDateTime = DateTime.Now,
                    TransactionLog = $"Delete the product : Product ID {targetProductToDelete.ProductID} ,Name : {targetProductToDelete.Name}"
                };

                unitOfWork.AdministratorsTransactionsRepository.Add(transaction);

                //in case of any exception occur while saving the data , non of those will be executed.
                unitOfWork.Save();

                TempData["RemoveResult"] = "Deleted";
            }
            catch (Exception) 
            {
                TempData["RemoveResult"] = "Error";

                var transaction = new AdministratorTransaction
                {
                    AdministratorID = CurrentAdministrator.UserID!,
                    TransactionDateTime = DateTime.Now,
                    TransactionLog = $"Try to delete the product : Product ID {targetProductToDelete.ProductID} ,Name : {targetProductToDelete.Name}"
                };

                unitOfWork.AdministratorsTransactionsRepository.Add(transaction);

                unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
