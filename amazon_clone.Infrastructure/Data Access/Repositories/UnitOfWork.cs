using amazon_clone.Infrastructure.Data_Access.Repositories;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public class UnitOfWork(DbContext context) : IUnitOfWork
    {

        public IProductRepository ProductRepository { get; set; } = new ProductRepository((AppDbContext)context);
        public ICustomerProductRepository CustomerProductRepository { get; set; } = new CustomerProductRepository((AppDbContext)context);
        public IClothesProductRepository ClothesProductRepository { get; set; } = new ClothesProductRepository((AppDbContext)context);
        public IPersonGenderRepository PersonGenderRepository { get; set; } = new PersonGenderRepository((AppDbContext)context);
        public IClothSizeRepository ClothSizeRepository { get; set; } = new ClothSizeRepository((AppDbContext)context);
        public IProductCategoryRepository ProductCategoryRepository { get; set; } = new ProductCategoryRepository((AppDbContext)context);
        public IWishListRepository WishListRepository { get; set; } = new WishListRepository((AppDbContext)context);
        public IWishListProductRepository WishListProductRepository { get; set; } = new WishListProductRepository((AppDbContext)context);
        public IOrderRepository OrderRepository { get; set; } = new OrderRepository((AppDbContext)context);
        public IOrderStatusRepository OrderStatusRepository { get; set; } = new OrderStatusRepository((AppDbContext)context);
        public IPromoCodeRepository PromoCodeRepository { get; set; } = new PromoCodeRepository((AppDbContext)context);
        public IShippingDetailRepository ShippingDetailRepository { get; set; } = new ShippingDetailRepository((AppDbContext)context);
        public IShoppingCartRepository ShoppingCartRepository { get; set; } = new ShoppingCartRepository((AppDbContext)context);
        public IShoppingCartProductRepository ShoppingCartProductRepository { get; set; } = new ShoppingCartProductRepository((AppDbContext)context);
        public IUsersRepository UsersRepository { get; set; } = new UsersRepository((AppDbContext)context);
        public IPaymentRepository PaymentRepository { get; set; } = new PaymentRepository((AppDbContext)context);


        public IAdministratorRepository AdministratorRepository { get; set; } = (context is DashboardDbContext) ? new AdministratorRepository((DashboardDbContext)context) : null!;
        public IAdministratorsOperationsRepository AdministratorsOperationsRepository { get; set; } = (context is DashboardDbContext) ? new AdministratorsOperationsRepository((DashboardDbContext)context) : null!;


        public void Save()
        {
            //transaction and logging here
            context.SaveChanges();
        }
    }
}
