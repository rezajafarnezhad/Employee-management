using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TAmountOfSalaryMapping : IEntityTypeConfiguration<TAmountOfSalary>
    {
        public void Configure(EntityTypeBuilder<TAmountOfSalary> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.Identity);

            builder.Property(c => c.HourOfOverTime).IsRequired();
            builder.Property(c => c.Insurance).IsRequired();
            builder.Property(c => c.RightChild).IsRequired();
            builder.Property(c => c.RightHousing).IsRequired();
            builder.Property(c => c.Salary).IsRequired();

        }
    }
}