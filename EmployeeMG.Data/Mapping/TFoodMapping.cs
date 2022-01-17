using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TFoodMapping : IEntityTypeConfiguration<TFood>
    {
        public void Configure(EntityTypeBuilder<TFood> builder)
        {
            builder.HasKey(c => c.Code);
            builder.Property(c => c.Code)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.Identity);

            builder.Property(c => c.FoodName).IsRequired().HasMaxLength(50);
        }
    }
}