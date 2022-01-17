using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TGoodsMapping : IEntityTypeConfiguration<TGoods>
    {
        public void Configure(EntityTypeBuilder<TGoods> builder)
        {
            builder.HasKey(c => c.Code);
            builder.Property(c => c.Code)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.None);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(60);
            builder.Property(c => c.Count).IsRequired();

        }
    }
}