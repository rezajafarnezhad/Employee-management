using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TEmployeeMapping : IEntityTypeConfiguration<TEmployee>
    {
        public void Configure(EntityTypeBuilder<TEmployee> builder)
        {
            builder.HasKey(c => c.PersonnelCode);
            builder.Property(c => c.PersonnelCode)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.Identity);

            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Sex).IsRequired().HasMaxLength(5);
            builder.Property(c => c.CodeMelli).IsRequired().HasMaxLength(11);
            builder.Property(c => c.FatherName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.DateOfBirth).IsRequired().HasMaxLength(50);
            builder.Property(c => c.DateOfEmployment).IsRequired().HasMaxLength(50);
            builder.Property(c => c.DegreeEducation).IsRequired().HasMaxLength(50);
            builder.Property(c => c.MaritalStatus).IsRequired().HasMaxLength(10);
            builder.Property(c => c.Mobile).IsRequired().HasMaxLength(15);
            builder.Property(c => c.Place).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(900);
            builder.Property(c => c.Picture).IsRequired();

            builder.HasOne(c => c.TUnit)
                .WithMany(c => c.TEmployees)
                .HasForeignKey(c => c.Unitid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.BankAccount)
                .WithOne(c => c.Employee)
                .HasForeignKey<TBankAccount>(c => c.PersonnelCode);

            builder.HasOne(c => c.TritionCard)
                .WithOne(c => c.TEmployee)
                .HasForeignKey<TNUTritionCard>(c => c.PersonnelCode);
        }
    }
}