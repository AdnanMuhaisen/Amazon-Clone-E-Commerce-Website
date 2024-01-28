using amazon_clone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class ShippingDetailsConfiguration : IEntityTypeConfiguration<ShippingDetail>
    {
        public void Configure(EntityTypeBuilder<ShippingDetail> builder)
        {
            builder.ToTable("tbl_ShippingDetails").HasKey(x => x.ID);

            builder.Property(x => x.ID).ValueGeneratedOnAdd();

            builder.OwnsOne(x => x.CreationDetails);

        }
    }
}
