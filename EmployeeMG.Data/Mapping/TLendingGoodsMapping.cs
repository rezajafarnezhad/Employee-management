using System.ComponentModel.DataAnnotations.Schema;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeMG.Data.Mapping
{
    public class TLendingGoodsMapping : IEntityTypeConfiguration<TLendingGoods>
    {
        public void Configure(EntityTypeBuilder<TLendingGoods> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired()
                .HasDefaultValue(DatabaseGeneratedOption.Identity);

            builder.Property(c => c.Count).IsRequired();

        }
    }
}