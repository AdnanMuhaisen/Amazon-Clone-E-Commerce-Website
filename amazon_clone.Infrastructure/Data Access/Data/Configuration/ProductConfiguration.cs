using amazon_clone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("tbl_Products").HasKey(p => p.ProductID);

            builder.UseTptMappingStrategy();

            builder.Property(x => x.ProductID)
                .ValueGeneratedOnAdd();

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.OwnsOne(p => p.ProductCreationDetails);

            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(p => p.DeleteDate)
                .HasDefaultValue(null);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.CategoryProducts)
                .HasForeignKey(p => p.CategoryID)
                .IsRequired(false);
        }
    }

}
