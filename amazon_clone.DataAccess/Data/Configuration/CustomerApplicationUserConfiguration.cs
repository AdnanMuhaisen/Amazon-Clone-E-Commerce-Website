using amazon_clone.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class CustomerApplicationUserConfiguration : IEntityTypeConfiguration<CustomerApplicationUser>
    {
        public void Configure(EntityTypeBuilder<CustomerApplicationUser> builder)
        {
            builder.OwnsOne(x => x.CreationDetails);
        }
    }
}
