using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.Roles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Infrastructure.DataAccess.Data.Contexts
{
    public class AppDbContext : IdentityDbContext<CustomerApplicationUser>
    {
        public AppDbContext() { }
        
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }
        public DbSet<ClothingProduct> ClothesProducts { get; set; }
        public DbSet<PersonGender> Genders { get; set; }
        public DbSet<ClothSize> ClothesSizes { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<WishListProduct> WishListsProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<ShippingDetail> ShippingDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartsProducts { get; set; }
        public DbSet<CustomerApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CustomerRole> ApplicationUserRoles { get; set; }
        public DbSet<Payment> Payments { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Ignore<Administrator>();
            modelBuilder.Ignore<AdministratorRole>();
            modelBuilder.Ignore<AdministratorOperation>();
        }
    }
}
