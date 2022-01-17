using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TCompanyMapping:IEntityTypeConfiguration<TCompany>
    {
        public void Configure(EntityTypeBuilder<TCompany> builder)
        {
            builder.HasKey(c => c.RegistrationNumber);
            builder.Property(c => c.RegistrationNumber)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.None)
                .HasMaxLength(50);
            builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Phone).IsRequired().HasMaxLength(30);
            builder.Property(c => c.WebSite).IsRequired(false).HasMaxLength(50);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(900);
            builder.Property(c => c.Logo).IsRequired();
        }
    }
}
