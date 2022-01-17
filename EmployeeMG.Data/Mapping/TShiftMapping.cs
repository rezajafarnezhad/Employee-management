using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TShiftMapping : IEntityTypeConfiguration<TShift>
    {
        public void Configure(EntityTypeBuilder<TShift> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.Identity);

            builder.Property(c => c.ShiftDate).IsRequired().HasMaxLength(50);
            builder.Property(c => c.ShiftDay).IsRequired().HasMaxLength(50);
            builder.Property(c => c.ShiftTime).IsRequired().HasMaxLength(50);
        }
    }
}