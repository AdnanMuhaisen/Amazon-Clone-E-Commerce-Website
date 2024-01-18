using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("tbl_Payments").HasKey(x => x.PaymentID);

            builder.Property(x => x.PaymentDateTime).HasDefaultValue(DateTime.Now);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.CustomerPayments)
                .HasForeignKey(x => x.CustomerID)
                .IsRequired(false);

            builder.HasOne(x => x.Order)
                .WithOne(x => x.Payment)
                .HasForeignKey<Payment>(x => x.OrderID)
                .IsRequired(false);
        }
    }
}
