using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("Customer");

            builder.Property(c=>c.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.PhoneNumber)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(c=>c.Email)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.Address)
                    .HasMaxLength(200)
                    .IsRequired();

            builder.HasOne(c=>c.Account)
                    .WithOne(a=>a.Customer)
                    .HasForeignKey<Account>(a => a.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
