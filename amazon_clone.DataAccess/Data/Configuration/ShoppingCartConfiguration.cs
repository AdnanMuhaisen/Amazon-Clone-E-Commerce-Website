using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("tbl_ShoppingCarts").HasKey(x => x.ShoppingCartID);

            builder.Property(x => x.ShoppingCartID).ValueGeneratedOnAdd();

            builder.HasMany(x => x.CartProducts)
                .WithMany(x => x.ShoppingCarts)
                .UsingEntity<ShoppingCartProduct>(
                l => l.HasOne(x => x.CustomerProduct).WithMany(x => x.ShoppingCartsProducts).HasForeignKey(x => x.CustomerProductID),
                r => r.HasOne(x => x.ShoppingCart).WithMany(x => x.ShoppingCartsProducts).HasForeignKey(x => x.ShoppingCartID)
                );

            builder.HasOne(x => x.CartPromoCode)
                .WithMany(x => x.ShoppingCarts)
                .HasForeignKey(x => x.PromoCodeID)
                .IsRequired(false);

            builder.OwnsOne(x => x.CreationDetails);
        }
    }
}
