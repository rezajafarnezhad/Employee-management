using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TFoodOfferedMapping : IEntityTypeConfiguration<TFoodOffered>
    {
        public void Configure(EntityTypeBuilder<TFoodOffered> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.Identity);

            builder.Property(c => c.OfferedDate).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Day).IsRequired().HasMaxLength(40);
            builder.Property(c => c.Meal).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Food).IsRequired().HasMaxLength(50);
        }
    }
}