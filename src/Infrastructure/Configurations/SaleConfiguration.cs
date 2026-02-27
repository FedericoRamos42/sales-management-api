using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Sale");


            builder.Property(s => s.TotalAmount)
                   .HasPrecision(18, 2);
                   
            builder.Property(s => s.PaidAmount)
                    .HasPrecision(18, 2);

            builder.Property(s => s.Status)
                   .HasConversion<string>();

            builder.HasOne(s => s.Customer)
                    .WithMany()
                    .HasForeignKey(s=>s.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Payments)
                    .WithOne()
                    .HasForeignKey(p => p.SaleId)
                    .OnDelete(DeleteBehavior.Cascade);

           

        }
    }
}
