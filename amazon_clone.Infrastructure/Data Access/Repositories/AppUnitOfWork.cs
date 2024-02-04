using amazon_clone.Infrastructure.DataAccess.Data.Contexts;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public class AppUnitOfWork(AppDbContext context) : IAppUnitOfWork
    {
        public IProductRepository ProductRepository { get; set; } =  new ProductRepository(context);
        public ICustomerProductRepository CustomerProductRepository { get; set; } = new CustomerProductRepository(context) ;
        public IClothesProductRepository ClothesProductRepository { get; set; } =  new ClothesProductRepository(context) ;
        public IPersonGenderRepository PersonGenderRepository { get; set; } =  new PersonGenderRepository(context) ;
        public IClothSizeRepository ClothSizeRepository { get; set; } =  new ClothSizeRepository(context);
        public IProductCategoryRepository ProductCategoryRepository { get; set; } =  new ProductCategoryRepository(context);
        public IWishListRepository WishListRepository { get; set; } = new WishListRepository(context) ;
        public IWishListProductRepository WishListProductRepository { get; set; } =  new WishListProductRepository(context) ;
        public IOrderRepository OrderRepository { get; set; } = new OrderRepository(context) ;
        public IOrderStatusRepository OrderStatusRepository { get; set; } =  new OrderStatusRepository(context);
        public IPromoCodeRepository PromoCodeRepository { get; set; } = new PromoCodeRepository(context);
        public IShippingDetailRepository ShippingDetailRepository { get; set; } = new ShippingDetailRepository(context) ;
        public IShoppingCartRepository ShoppingCartRepository { get; set; } = new ShoppingCartRepository(context) ;
        public IShoppingCartProductRepository ShoppingCartProductRepository { get; set; } = new ShoppingCartProductRepository(context) ;
        public IUsersRepository UsersRepository { get; set; } = new UsersRepository(context);
        public IPaymentRepository PaymentRepository { get; set; } =new PaymentRepository(context);

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
