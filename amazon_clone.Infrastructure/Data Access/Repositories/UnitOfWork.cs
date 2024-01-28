using amazon_clone.Infrastructure.DataAccess.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public class UnitOfWork(AppDbContext _appDbContext, DashboardDbContext _dashboardDbContext) : IUnitOfWork
    {
        public DbContext appDbContext { get; set; } = _appDbContext;
        public DashboardDbContext dashboardDbContext { get; set; } = _dashboardDbContext;

        public IProductRepository ProductRepository { get; set; } = new ProductRepository(_appDbContext);
        public ICustomerProductRepository CustomerProductRepository { get; set; } = new CustomerProductRepository(_appDbContext);
        public IClothesProductRepository ClothesProductRepository { get; set; } = new ClothesProductRepository(_appDbContext);
        public IPersonGenderRepository PersonGenderRepository { get; set; } = new PersonGenderRepository(_appDbContext);
        public IClothSizeRepository ClothSizeRepository { get; set; } = new ClothSizeRepository(_appDbContext);
        public IProductCategoryRepository ProductCategoryRepository { get; set; } = new ProductCategoryRepository(_appDbContext);
        public IWishListRepository WishListRepository { get; set; } = new WishListRepository((AppDbContext)_appDbContext);
        public IWishListProductRepository WishListProductRepository { get; set; } = new WishListProductRepository(_appDbContext);
        public IOrderRepository OrderRepository { get; set; } = new OrderRepository((AppDbContext)_appDbContext);
        public IOrderStatusRepository OrderStatusRepository { get; set; } = new OrderStatusRepository(_appDbContext);
        public IPromoCodeRepository PromoCodeRepository { get; set; } = new PromoCodeRepository(_appDbContext);
        public IShippingDetailRepository ShippingDetailRepository { get; set; } = new ShippingDetailRepository(_appDbContext);
        public IShoppingCartRepository ShoppingCartRepository { get; set; } = new ShoppingCartRepository(_appDbContext);
        public IShoppingCartProductRepository ShoppingCartProductRepository { get; set; } = new ShoppingCartProductRepository(_appDbContext);
        public IUsersRepository UsersRepository { get; set; } = new UsersRepository(_appDbContext);
        public IPaymentRepository PaymentRepository { get; set; } = new PaymentRepository(_appDbContext);
        public IAdministratorRepository AdministratorRepository { get; set; } = new AdministratorRepository(_dashboardDbContext);
        //public IAdministratorsTransactionsRepository AdministratorsTransactionsRepository { get; set; } = new AdministratorsTransactionsRepository(_dashboardDbContext);

        public void Save()
        {
            //transaction and logging here

            dashboardDbContext.SaveChanges();
            appDbContext.SaveChanges();
        }
    }
}
