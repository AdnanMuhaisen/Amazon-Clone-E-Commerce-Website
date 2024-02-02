using amazon_clone.Domain.Models;
using amazon_clone.Domain.Users.Roles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Infrastructure.DataAccess.Data.Contexts
{
    public class DashboardDbContext : IdentityDbContext<Administrator>
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<AdministratorRole> ApplicationUserRoles { get; set; }
        public DbSet<AdministratorOperation> AdministratorOperations { get; set; }


        public DashboardDbContext() { }
        public DashboardDbContext(DbContextOptions<DashboardDbContext> dbContextOptions) : base(dbContextOptions) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server = .; Database = amazon_clone_dashboard; Integrated Security = SSPI; TrustServerCertificate = True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // load the configurations
            var assembly = typeof(DashboardDbContext).Assembly;
            builder.ApplyConfigurationsFromAssembly(assembly);

            //ignore the AppDbContextModels
            builder.Ignore<Product>();
            builder.Ignore<CustomerProduct>();
            builder.Ignore<PersonGender>();
            builder.Ignore<ClothSize>();
            builder.Ignore<ProductCategory>();
            builder.Ignore<WishList>();
            builder.Ignore<WishListProduct>();
            builder.Ignore<Order>();
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
            builder.Ignore<ClothesSizes>();

            
        }
    }
}
