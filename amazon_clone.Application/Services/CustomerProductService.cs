using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Domain.View_Models;
using amazon_clone.Infrastructure.Data_Access.Repositories;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.App_Details;
using amazon_clone.Utility.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Application.Services
{
    public class CustomerProductService : ICustomerProductService
    {
        private readonly IAdministratorOperationService administratorOperationService;
        private readonly IDashboardUnitOfWork dashboardUnitOfWork;

        public IAppUnitOfWork _unitOfWork { get; }

        public CustomerProductService(IAppUnitOfWork unitOfWork,IAdministratorOperationService administratorOperationService,IDashboardUnitOfWork dashboardUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            this.administratorOperationService = administratorOperationService;
            this.dashboardUnitOfWork = dashboardUnitOfWork;
        }

        public IEnumerable<CustomerProduct> GetAllCustomerProducts()
        {
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

            ArgumentNullException.ThrowIfNull(customerProducts);

            return customerProducts;
        }

        public IEnumerable<CustomerProduct> GetSearchResult(string valueToSearch)
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

            var searchResult = customerProducts!
                .Where(x => x.Name.Contains(valueToSearch, StringComparison.OrdinalIgnoreCase));

            if (searchResult is null)
            {
                searchResult = Enumerable.Empty<CustomerProduct>();
            }

            return searchResult.ToList();
        }

        public IEnumerable<CustomerProduct> GetAllElectronicProducts()
        {
            var electronicProducts = _unitOfWork
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
                });

            if (electronicProducts is null)
            {
                electronicProducts = Enumerable.Empty<CustomerProduct>();
            }

            return electronicProducts.ToList()!;
        }

        public IEnumerable<CustomerProduct> GetAllJewelryProducts()
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
                });

            if (jewelryCustomerProducts is null)
            {
                jewelryCustomerProducts = Enumerable.Empty<CustomerProduct>();
            }

            return jewelryCustomerProducts.ToList()!;
        }

        public CustomerProduct GetByID(int ProductID)
        {
            var customerProduct = _unitOfWork
                .CustomerProductRepository
                .GetAsNoTracking(filter: x => x.ProductID == ProductID);

            if (customerProduct is null)
            {
                return default!;
            }
            customerProduct!.ImageUrl = @"/images/products/" + customerProduct.ImageUrl;

            return customerProduct;
        }

        public IEnumerable<CustomerProduct> GetAllCustomerProductsWithAllDetails()
        {
            var customerProducts = _unitOfWork
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
                    });

            if (customerProducts is null)
            {
                customerProducts = Enumerable.Empty<CustomerProduct>();
            }

            return customerProducts.ToList();
        }

        public CustomerProductViewModel GetCustomerProductForUpsert(int? ProductID)
        {
            var productCategories = _unitOfWork
                .ProductCategoryRepository
                .GetAllAsNoTracking()?
                .Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryID.ToString()
                });

            ArgumentNullException.ThrowIfNull(productCategories);

            if (!productCategories.Any())
            {
                productCategories = Enumerable.Empty<SelectListItem>();
            }

            CustomerProductViewModel customerProductViewModel;
            if (ProductID is not null)
            {
                var targetProductToUpdate = _unitOfWork
                    .CustomerProductRepository
                    .Get(x => x.ProductID == ProductID);

                ArgumentNullException.ThrowIfNull(targetProductToUpdate);

                customerProductViewModel = new CustomerProductViewModel(productCategories, targetProductToUpdate);
            }
            else
            {
                customerProductViewModel = new CustomerProductViewModel(productCategories, default!);
            }

            return customerProductViewModel;
        }

        public void UpsertCustomerProduct(CustomerProduct customerProduct, IFormFile formFile)
        {
            if (formFile is not null)
            {
                // because of when we need to decode this we need the actual name of the file to add it to the wwwroot folder
                customerProduct.ImageUrl = formFile.FileName.ToBase64String();
            }

            if (customerProduct.ProductID == 0)
            {
                // insert

                int lastCustomerProductID = _unitOfWork
                    .CustomerProductRepository
                    .GetAllAsNoTracking()!
                    .Select(x => x.CustomerProductID)
                    .MaxBy(x => x);

                customerProduct.CustomerProductID = ++lastCustomerProductID;
                _unitOfWork.CustomerProductRepository.Add(customerProduct);

                administratorOperationService.AddNewAdministratorOperation(dashboardUnitOfWork,
                        CurrentAdministrator.UserID!,
                        DateTime.Now,
                        $"Add the product : product name is {customerProduct.Name}");

                _unitOfWork.CustomerProductRepository.Add(customerProduct);

                _unitOfWork.Save();
                dashboardUnitOfWork.Save();
            }
            else
            {
                //update
                var targetCustomerProductToUpdate = _unitOfWork
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

                administratorOperationService.AddNewAdministratorOperation(dashboardUnitOfWork,
                        CurrentAdministrator.UserID!,
                        DateTime.Now,
                         $"Update the product : product name is {customerProduct.Name}");

                _unitOfWork.Save();
                dashboardUnitOfWork.Save();
            }
        }

        public void RemoveCustomerProduct(int ProductID)
        {
            var targetProductToDelete = _unitOfWork
                .CustomerProductRepository
                .Get(filter: x => x.ProductID == ProductID);

            ArgumentNullException.ThrowIfNull(targetProductToDelete);

            _unitOfWork.ProductRepository.Remove((Product)targetProductToDelete);

            administratorOperationService.AddNewAdministratorOperation(dashboardUnitOfWork,
                CurrentAdministrator.UserID!,
                DateTime.Now,
                $"Delete the product : Product ID {targetProductToDelete.ProductID} ,Name : {targetProductToDelete.Name}");

            //in case of any exception occur while saving the data , non of those will be executed.
            _unitOfWork.Save();
            dashboardUnitOfWork.Save();
        }

    }
}
