using amazon_clone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.Infrastructure.DataAccess.Data.Configuration
{
    public class AdministratorOperationConfiguration : IEntityTypeConfiguration<AdministratorOperation>
    {
        public void Configure(EntityTypeBuilder<AdministratorOperation> builder)
        {
            builder.ToTable("tbl_AdministratorOperations").HasKey(x => x.OperationID);

            builder.Property(x => x.OperationDateTime).HasDefaultValue(DateTime.UtcNow);

            builder.HasOne(x => x.Administrator)
                .WithMany(x => x.AdministratorTransactions)
                .HasForeignKey(x => x.AdministratorID)
                .IsRequired(true);
        }
    }
}
