using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class ClothSizeConfiguration : IEntityTypeConfiguration<ClothSize>
    {
        public void Configure(EntityTypeBuilder<ClothSize> builder)
        {
            builder.ToTable("tbl_ClothesSizes").HasKey(c => c.SizeID);

            builder.Property(p => p.SizeID).ValueGeneratedOnAdd();
            builder.Property(p => p.SizeID).ValueGeneratedOnAdd();
            builder.OwnsOne(x => x.SizeCreationDetails);

            builder.Property(x => x.SizeID)
                .ValueGeneratedNever();

            builder.Property(x => x.Size)
                .HasMaxLength(5);
        }
    }  



}
