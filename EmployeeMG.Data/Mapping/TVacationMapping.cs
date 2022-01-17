using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TVacationMapping : IEntityTypeConfiguration<TVacation>
    {
        public void Configure(EntityTypeBuilder<TVacation> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.Identity);

            builder.Property(c => c.FromDate).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CountDate).IsRequired();
        }
    }
}