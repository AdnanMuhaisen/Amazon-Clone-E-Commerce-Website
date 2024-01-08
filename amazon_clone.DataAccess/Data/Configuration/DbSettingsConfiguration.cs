using amazon_clone.Models.General_Database_Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class DbSettingsConfiguration : IEntityTypeConfiguration<DbSettings>
    {
        public void Configure(EntityTypeBuilder<DbSettings> builder)
        {
            builder.ToTable("tbl_DbSettings").HasKey(x => x.ID);
            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.OwnsOne(x => x.InsertionHelper, c =>
            {
                c.Property(x => x.LastInsertedCustomerID).HasColumnName("LastInsertedCustomerID");
            });
        }
    }
}
