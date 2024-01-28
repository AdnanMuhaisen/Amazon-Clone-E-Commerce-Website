using amazon_clone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class PersonGenderConfiguration : IEntityTypeConfiguration<PersonGender>
    {
        public void Configure(EntityTypeBuilder<PersonGender> builder)
        {
            builder.ToTable("tbl_Genders").HasKey(g => g.GenderID);

            builder.Property(p => p.GenderID).ValueGeneratedOnAdd();

            builder.Property(g => g.GenderID)
                .ValueGeneratedOnAdd();

            builder.Property(g => g.Gender)
                .HasMaxLength(10);
        }
    }



}
