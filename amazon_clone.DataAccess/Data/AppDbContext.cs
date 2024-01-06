using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace amazon_clone.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }
        public DbSet<ClothesProduct> ClothesProducts { get; set; }
        public DbSet<PersonGender> Genders { get; set; }
        public DbSet<ClothSize> ClothesSizes { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<WishListProduct> WishListsProducts { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // this should be removed
            #region ForMigrations
            var content = new ConfigurationBuilder()
                .AddJsonFile(@"Data\appsettings.json")
                .Build();

            var ConnectionString = content.GetSection("ConnectionString").Value;
            optionsBuilder
                .UseSqlServer(ConnectionString);

            #endregion
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
