using amazon_clone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("tbl_OrderStatuses").HasKey(x => x.StatusID);

            builder.Property(x => x.StatusID).ValueGeneratedOnAdd();

            builder.OwnsOne(x => x.CreationDetails);

        }
    }

}
