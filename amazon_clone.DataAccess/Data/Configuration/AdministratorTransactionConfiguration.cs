using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class AdministratorTransactionConfiguration : IEntityTypeConfiguration<AdministratorTransaction>
    {
        public void Configure(EntityTypeBuilder<AdministratorTransaction> builder)
        {
            builder.ToTable("tbl_AdministratorTransactions").HasKey(x => x.TransactionID);

            builder.Property(x => x.TransactionDateTime).HasDefaultValue(DateTime.UtcNow);

            builder.HasOne(x => x.Administrator)
                .WithMany(x => x.AdministratorTransactions)
                .HasForeignKey(x => x.AdministratorID)
                .IsRequired(false);
        }
    }
}
