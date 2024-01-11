using amazon_clone.DataAccess.Interceptors;
using amazon_clone.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace amazon_clone.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext() { }
        
        public AppDbContext(DbContextOptions options):base(options) { }
        
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
        public DbSet<Person> Persons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // this should be removed
            #region ForMigrations
            var content = new ConfigurationBuilder()
                .AddJsonFile(@"Data\dataaccess_appsettings.json")
                .Build();

            var ConnectionString = content.GetSection("ConnectionString").Value;
            optionsBuilder
                .UseSqlServer(ConnectionString)
                .AddInterceptors(new List<SaveChangesInterceptor>
                {
                    new SoftDeleteInterceptor()
                });

            #endregion
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
