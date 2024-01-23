using amazon_clone.Models.General_Database_Settings;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users.Roles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Stripe.Climate;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace amazon_clone.DataAccess.Data.Contexts
{
    public class DashboardDbContext : IdentityDbContext<Administrator>
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<AdministratorRole> ApplicationUserRoles { get; set; }
        public DbSet<AdministratorTransaction> AdministratorTransactions { get; set; }



        public DashboardDbContext()
        {
            
        }

        public DashboardDbContext(DbContextOptions<DashboardDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // load the configurations
            var assembly = typeof(DashboardDbContext).Assembly;
            builder.ApplyConfigurationsFromAssembly(assembly);

            //ignore the AppDbContextModels
            builder.Ignore<amazon_clone.Models.Models.Product>();
            builder.Ignore<CustomerProduct>();
            builder.Ignore<PersonGender>();
            builder.Ignore<ClothSize>();
            builder.Ignore<ProductCategory>();
            builder.Ignore<WishList>();
            builder.Ignore<WishListProduct>();
            builder.Ignore<amazon_clone.Models.Models.Order>();
            builder.Ignore<OrderStatus>();
            builder.Ignore<PromoCode>();
            builder.Ignore<ShippingDetail>();
            builder.Ignore<ShoppingCart>();
            builder.Ignore<ShoppingCartProduct>();
            builder.Ignore<CustomerApplicationUser>();
            builder.Ignore<CustomerRole>();
            builder.Ignore<Payment>();
            builder.Ignore<CreationDetails>();
            builder.Ignore<ClothingProduct>();
            builder.Ignore<DbSettings>();
            builder.Ignore<ClothesSizes>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
