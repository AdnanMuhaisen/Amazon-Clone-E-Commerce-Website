using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("tbl_Orders").HasKey(x => x.OrderID);

            builder.Property(x => x.OrderID).ValueGeneratedOnAdd();

            builder.OwnsOne(x => x.CreationDetails);

        }
    }
}
