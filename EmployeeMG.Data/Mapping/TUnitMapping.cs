using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TUnitMapping : IEntityTypeConfiguration<TUnit>
    {
        public void Configure(EntityTypeBuilder<TUnit> builder)
        {
            builder.HasKey(c => c.Code);
            builder.Property(c => c.Code)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.Identity);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);

            builder.HasMany(c => c.TEmployees)
                .WithOne(c => c.TUnit)
                .HasForeignKey(c => c.Unitid)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(c => c.TAmountOfSalary)
                .WithOne(c => c.TUnit)
                .HasForeignKey<TAmountOfSalary>(c => c.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}