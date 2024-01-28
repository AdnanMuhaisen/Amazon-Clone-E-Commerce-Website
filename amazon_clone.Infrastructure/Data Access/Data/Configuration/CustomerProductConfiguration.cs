using amazon_clone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class CustomerProductConfiguration : IEntityTypeConfiguration<CustomerProduct>
    {
        public void Configure(EntityTypeBuilder<CustomerProduct> builder)
        {
            builder.ToTable("tbl_CustomerProducts");//.HasKey(x=>x.CustomerProductID);

            builder.Property(p => p.CustomerProductID).ValueGeneratedOnAdd();
        }
    }



}
