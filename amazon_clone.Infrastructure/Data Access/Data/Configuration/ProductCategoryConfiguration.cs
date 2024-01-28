using amazon_clone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("tbl_ProductCategories").HasKey(p => p.CategoryID);

            builder.Property(p => p.CategoryID).ValueGeneratedOnAdd();

            builder.OwnsOne(c => c.CategoryCreationDetails);
        }
    }



}
