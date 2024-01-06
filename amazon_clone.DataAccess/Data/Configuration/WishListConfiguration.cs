using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    internal class WishListConfiguration : IEntityTypeConfiguration<WishList>
    {
        public void Configure(EntityTypeBuilder<WishList> builder)
        {
            builder.ToTable("tbl_WishLists").HasKey(x => x.WishListID);
            builder.Property(x => x.WishListID).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Products)
                .WithMany(x => x.ProductWishLists)
                .UsingEntity<WishListProduct>(
                l => l.HasOne(x => x.CustomerProduct).WithMany(x => x.WishListsProducts).HasForeignKey(x => x.ProductID),
                r => r.HasOne(x => x.WishList).WithMany(x => x.WishListsProducts).HasForeignKey(x => x.ListID)
                );

            builder.OwnsOne(x => x.CreationDetails);
        }
    }
}
