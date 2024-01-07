using amazon_clone.DataAccess.Data;

namespace amazon_clone.DataAccess.Repositories
{
    public class UnitOfWork(
        AppDbContext _context,
        IProductRepository _ProductRepository,
        ICustomerProductRepository _CustomerProductRepository,
        IClothesProductRepository _ClothesProductRepository,
        IPersonGenderRepository _PersonGenderRepository,
        IClothSizeRepository _ClothSizeRepository,
        IProductCategoryRepository _ProductCategoryRepository,
        IWishListRepository _WishListRepository,
        IWishListProductRepository _WishListProductRepository,
        IOrderRepository _OrderRepository,
        IOrderStatusRepository _OrderStatusRepository,
        IPromoCodeRepository _PromoCodeRepository,
        IShippingDetailRepository _ShippingDetailRepository,
        IShoppingCartRepository _ShoppingCartRepository,
        IShoppingCartProductRepository _ShoppingCartProductRepository) 
        : IUnitOfWork
    {
        public AppDbContext context { get; set; } = _context;
        public IProductRepository ProductRepository { get; set; } = _ProductRepository;
        public ICustomerProductRepository CustomerProductRepository { get; set; } = _CustomerProductRepository;
        public IClothesProductRepository ClothesProductRepository { get; set; } = _ClothesProductRepository;
        public IPersonGenderRepository PersonGenderRepository { get; set; } = _PersonGenderRepository;
        public IClothSizeRepository ClothSizeRepository { get; set; } = _ClothSizeRepository;
        public IProductCategoryRepository ProductCategoryRepository { get; set; } = _ProductCategoryRepository;
        public IWishListRepository WishListRepository { get; set; } = _WishListRepository;
        public IWishListProductRepository WishListProductRepository { get; set; } = _WishListProductRepository;
        public IOrderRepository OrderRepository { get; set; } = _OrderRepository;
        public IOrderStatusRepository OrderStatusRepository { get; set; } = _OrderStatusRepository;
        public IPromoCodeRepository PromoCodeRepository { get; set; } = _PromoCodeRepository;
        public IShippingDetailRepository ShippingDetailRepository { get; set; } = _ShippingDetailRepository;
        public IShoppingCartRepository ShoppingCartRepository { get; set; } = _ShoppingCartRepository;
        public IShoppingCartProductRepository ShoppingCartProductRepository { get; set; } = _ShoppingCartProductRepository;

        public void Save()
        {
            //transaction and logging here
            context.SaveChanges();
        }
    }
}
