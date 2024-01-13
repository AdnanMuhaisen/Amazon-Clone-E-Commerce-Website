using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class WishListProductConfiguration : IEntityTypeConfiguration<WishListProduct>
    {
        public void Configure(EntityTypeBuilder<WishListProduct> builder)
        {
            builder.ToTable("tbl_WishListsProducts").HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            
        }
    }
}
