using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TCostMapping : IEntityTypeConfiguration<TCost>
    {
        public void Configure(EntityTypeBuilder<TCost> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.Identity);

            builder.Property(c => c.CostDate).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Forthe).IsRequired().HasMaxLength(250);
            builder.Property(c => c.Amount).IsRequired();
        }
    }
}