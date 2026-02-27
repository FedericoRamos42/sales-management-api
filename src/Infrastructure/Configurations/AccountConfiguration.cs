using Domain.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);
            builder.ToTable("Account");

            builder.Property(c => c.Balance)
                   .HasPrecision(18,2)
                   .IsRequired();

            builder.HasIndex(a => a.CustomerId)
                   .IsUnique();

            builder.HasMany(a => a.Movements)
                   .WithOne(m => m.Account)
                   .HasForeignKey(m => m.AccountId)
                   .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
