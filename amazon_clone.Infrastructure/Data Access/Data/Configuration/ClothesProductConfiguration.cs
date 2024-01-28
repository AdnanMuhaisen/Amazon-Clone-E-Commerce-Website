using amazon_clone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class ClothesProductConfiguration : IEntityTypeConfiguration<ClothingProduct>
    {
        public void Configure(EntityTypeBuilder<ClothingProduct> builder)
        {
            builder.ToTable("tbl_ClothesProducts");//.HasKey(x => x.ClothesProductID);

            builder.Property(c => c.ClothingProductID)
                .ValueGeneratedOnAdd();

            builder.HasOne(c => c.TargetGender)
                .WithMany(g => g.ClothesProducts)
                .HasForeignKey(c => c.TargetGenderID)
                .IsRequired(false);

            builder.HasMany(c => c.Sizes)
                .WithMany(s => s.ClothesProducts)
                .UsingEntity<ClothesSizes>(
                l => l.HasOne(e => e.ClothesSize).WithMany(e => e.ClothesSizes).HasForeignKey(e => e.ClothesSizeID),
                r => r.HasOne(e => e.ClothesProduct).WithMany(x => x.ClothesSizes).HasForeignKey(x => x.ClothesProductID)
                );
        }
    }



}
