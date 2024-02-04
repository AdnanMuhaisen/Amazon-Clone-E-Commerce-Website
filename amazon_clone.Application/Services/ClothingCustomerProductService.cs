using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.App_Details;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Application.Services
{
    public class ClothingCustomerProductService : IClothingCustomerProductService
    {
        public IAppUnitOfWork _unitOfWork { get; }
        public ClothingCustomerProductService(IAppUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<ClothingProduct> GetAllMenClothingProducts()
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
                });

            if (menClothingProducts is null)
            {
                menClothingProducts = Enumerable.Empty<ClothingProduct>();
            }

            return menClothingProducts.ToList();
        }

        public IEnumerable<ClothingProduct> GetAllWomenClothingProducts()
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
                });

            if (womenClothingProducts is null)
            {
                womenClothingProducts = Enumerable.Empty<ClothingProduct>();
            }

            return womenClothingProducts.ToList();
        }

        public ClothingProduct? Get(int ProductID, int ClothingProductID)
        {
            var clothingProduct = _unitOfWork
            .ClothesProductRepository
            .GetAllAsNoTracking(x => x.ProductID == ProductID && x.ClothingProductID == ClothingProductID,
            IncludeProperties: "Sizes")?
            .Select(p => new ClothingProduct
            {
                ProductID = p.ProductID,
                Name = (p.Name.Length > 25) ? p.Name.Substring(0, 25) + "..." : p.Name,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity,
                // if there is no image for the product set the default product image. 
                ImageUrl = @"/images/products/" + p.ImageUrl ?? StaticDetails.DEFAULT_PRODUCT_IMAGE,
                CustomerProductID = p.CustomerProductID,
                ClothingProductID = p.ClothingProductID,
                Sizes = p.Sizes
            })
            .FirstOrDefault();

            return clothingProduct;
        }
    }
}
