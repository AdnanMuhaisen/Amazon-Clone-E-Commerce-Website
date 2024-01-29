using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.App_Details;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Application.Services
{
    public class CustomerProductService : ICustomerProductService
    {
        public IUnitOfWork _unitOfWork { get; }

        public CustomerProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
