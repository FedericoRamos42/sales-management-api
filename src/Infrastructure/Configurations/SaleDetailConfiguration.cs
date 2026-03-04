using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.HasKey(b => b.Id);

            builder.ToTable("SaleDetail");

            builder.Property(p => p.ProductId)
                    .IsRequired();

            builder.Property(p=>p.Total)
                    .HasColumnType("decimal(18,2)")    
                    .IsRequired();

            builder.Property(p => p.Quantity)
                   .IsRequired();

            builder.Property(p => p.UnitPrice)
                    .IsRequired();

            builder.HasOne(p => p.Product)
                   .WithMany()
                   .HasForeignKey(d=>d.ProductId);

            builder.HasOne(p => p.Sale)
                    .WithMany(s=>s.Items)
                    .HasForeignKey(p=>p.SaleId);
           
        }
    }
}
