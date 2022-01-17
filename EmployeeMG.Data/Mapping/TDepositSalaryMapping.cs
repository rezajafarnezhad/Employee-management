using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TDepositSalaryMapping : IEntityTypeConfiguration<TDepositSalary>
    {
        public void Configure(EntityTypeBuilder<TDepositSalary> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Year).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Month).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Amount).IsRequired();
        }
    }
}