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
    public class AccountMovementConfiguration : IEntityTypeConfiguration<AccountMovement>
    {
        public void Configure(EntityTypeBuilder<AccountMovement> builder)
        {
            builder.HasKey(m => m.Id);
            builder.ToTable("AccountMovement");

            builder.Property(m => m.Amount)
                   .HasPrecision(18, 2);

            builder.Property(m=>m.BalanceAfter)
                   .HasPrecision(18, 2);

            builder.Property(m => m.Description)
                   .IsRequired()
                   .HasMaxLength(250);

        }
    }
}
