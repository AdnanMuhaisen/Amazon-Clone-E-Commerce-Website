namespace amazon_clone.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; set; }
        public ICustomerProductRepository CustomerProductRepository { get; set; }
        public IClothesProductRepository ClothesProductRepository { get; set; }
        public IPersonGenderRepository PersonGenderRepository { get; set; }
        public IClothSizeRepository ClothSizeRepository { get; set; }
        public IProductCategoryRepository ProductCategoryRepository { get; set; }
        public IWishListRepository WishListRepository { get; set; }
        public IWishListProductRepository WishListProductRepository { get; set; }
        public IOrderRepository OrderRepository { get; set; }
        public IOrderStatusRepository OrderStatusRepository { get; set; }
        public IPromoCodeRepository PromoCodeRepository { get; set; }
        public IShippingDetailRepository ShippingDetailRepository { get; set; }
        public IShoppingCartRepository ShoppingCartRepository { get; set; }
        public IShoppingCartProductRepository ShoppingCartProductRepository { get; set; }
        public IDbSettingsRepository DbSettingsRepository { get; set; }
        public IUsersRepository UsersRepository { get; set; }

        void Save();
    }
}
