using Domain.Enitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Token)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(r => r.Admin)
                .WithMany()
                .HasForeignKey(r => r.AdminId);

        }
    }
}
