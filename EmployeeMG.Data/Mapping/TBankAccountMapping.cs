using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TBankAccountMapping : IEntityTypeConfiguration<TBankAccount>
    {
        public void Configure(EntityTypeBuilder<TBankAccount> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.None);

            builder.Property(c => c.Shaba).IsRequired().HasMaxLength(50);
        }
    }
}