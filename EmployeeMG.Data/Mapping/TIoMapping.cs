using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TIoMapping : IEntityTypeConfiguration<TIo>
    {
        public void Configure(EntityTypeBuilder<TIo> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.Identity);

            builder.Property(c => c.DateOfDay).IsRequired().HasMaxLength(50);
            builder.Property(c => c.EntranceTime).IsRequired().HasMaxLength(50);
            builder.Property(c => c.ExitTime).IsRequired().HasMaxLength(10);
        }
    }
}