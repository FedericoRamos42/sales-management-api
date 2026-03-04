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
    public class CalendarEventConfiguration : IEntityTypeConfiguration<CalendarEvent>
    {
        public void Configure(EntityTypeBuilder<CalendarEvent> builder)
        {
            builder.ToTable("CalendarEvent");
            builder.HasKey(e => e.Id);

            builder.Property(e=>e.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e=>e.Description)
                .HasMaxLength(200);

            builder.Property(e => e.Type)
                   .IsRequired()
                   .HasConversion<string>();
            builder.Property(e => e.StartDate)
                    .IsRequired();

            builder.Property(e => e.EndDate);

            builder.HasOne(e => e.Admin)
                    .WithMany()
                    .HasForeignKey(e => e.AdminId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
