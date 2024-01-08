using amazon_clone.DataAccess.Data;

namespace amazon_clone.DataAccess.Repositories
{
    public class UnitOfWork(AppDbContext _context)

        : IUnitOfWork
    {
        public AppDbContext context { get; set; } = _context;
        public IProductRepository ProductRepository { get; set; } = new ProductRepository(_context);
        public ICustomerProductRepository CustomerProductRepository { get; set; } = new CustomerProductRepository(_context);
        public IClothesProductRepository ClothesProductRepository { get; set; } = new ClothesProductRepository(_context);
        public IPersonGenderRepository PersonGenderRepository { get; set; } = new PersonGenderRepository(_context);
        public IClothSizeRepository ClothSizeRepository { get; set; } = new ClothSizeRepository(_context);
        public IProductCategoryRepository ProductCategoryRepository { get; set; } = new ProductCategoryRepository(_context);
        public IWishListRepository WishListRepository { get; set; } = new WishListRepository(_context);
        public IWishListProductRepository WishListProductRepository { get; set; } = new WishListProductRepository(_context);
        public IOrderRepository OrderRepository { get; set; } = new OrderRepository(_context);
        public IOrderStatusRepository OrderStatusRepository { get; set; } = new OrderStatusRepository(_context);
        public IPromoCodeRepository PromoCodeRepository { get; set; } = new PromoCodeRepository(_context);
        public IShippingDetailRepository ShippingDetailRepository { get; set; } = new ShippingDetailRepository(_context);
        public IShoppingCartRepository ShoppingCartRepository { get; set; } = new ShoppingCartRepository(_context);
        public IShoppingCartProductRepository ShoppingCartProductRepository { get; set; } = new ShoppingCartProductRepository(_context);
        public IDbSettingsRepository DbSettingsRepository { get; set; } = new DbSettingsRepository(_context);

        public void Save()
        {
            //transaction and logging here
            context.SaveChanges();
        }
    }
}
