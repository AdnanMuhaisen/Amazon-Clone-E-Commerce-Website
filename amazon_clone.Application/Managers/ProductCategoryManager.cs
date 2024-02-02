using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;

namespace amazon_clone.Application.Managers
{
    public class ProductCategoryManager : IProductCategoryManager
    {
        private readonly IAdministratorOperationService administratorOperationService;

        public IUnitOfWork UnitOfWork { get; }
        public ProductCategoryManager(IUnitOfWork unitOfWork, IAdministratorOperationService administratorOperationService)
        {
            UnitOfWork = unitOfWork;
            this.administratorOperationService = administratorOperationService;
        }


        public IEnumerable<ProductCategory> GetAllCategories()
        {
            var productCategories = UnitOfWork
                .ProductCategoryRepository
                .GetAllAsNoTracking();

            if (productCategories is null)
            {
                productCategories = Enumerable.Empty<ProductCategory>();
            }

            return productCategories.ToList();
        }

        public void UpsertProductCategory(ProductCategory productCategory)
        {
            if (productCategory.CategoryID == 0)
            {
                //insert
                UnitOfWork.ProductCategoryRepository.Add(productCategory);

                administratorOperationService.AddNewAdministratorOperation(UnitOfWork,
                    CurrentAdministrator.UserID!,
                    DateTime.Now,
                    $"The product category : Category Name : {productCategory.CategoryName} is added");

                UnitOfWork.Save();
            }
            else
            {
                //update
                UnitOfWork.ProductCategoryRepository.Update(productCategory);

                administratorOperationService.AddNewAdministratorOperation(UnitOfWork,
                        CurrentAdministrator.UserID!,
                        DateTime.Now,
                        $"Update the product category : Category Name : {productCategory.CategoryName}");

                UnitOfWork.Save();
            }
        }

        public void RemoveProductCategory(int CategoryID)
        {
            var targetCategoryToDelete = UnitOfWork
                .ProductCategoryRepository
                .Get(x => x.CategoryID == CategoryID);

            ArgumentNullException.ThrowIfNull(targetCategoryToDelete);

            UnitOfWork.ProductCategoryRepository.Remove(targetCategoryToDelete);

            administratorOperationService.AddNewAdministratorOperation(UnitOfWork,
                CurrentAdministrator.UserID!,
                DateTime.Now,
                $"Delete the product category : Category Name : {targetCategoryToDelete.CategoryName}");

            UnitOfWork.Save();
        }

    }
}
