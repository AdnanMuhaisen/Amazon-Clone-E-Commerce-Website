using amazon_clone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class PromoCodeConfiguration : IEntityTypeConfiguration<PromoCode>
    {
        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder.ToTable("tbl_PromoCodes").HasKey(x => x.CodeID);

            builder.Property(x => x.CodeID).ValueGeneratedOnAdd();

            builder.OwnsOne(x => x.CreationDetails);

        }
    }
}
