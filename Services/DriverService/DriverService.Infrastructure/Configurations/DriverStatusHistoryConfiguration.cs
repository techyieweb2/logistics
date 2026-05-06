using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Infrastructure.Configurations;

public class DriverStatusHistoryConfiguration : IEntityTypeConfiguration<DriverStatusHistory>
{
    public void Configure(EntityTypeBuilder<DriverStatusHistory> builder)
    {
        builder.ToTable("DriverStatusHistories");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(s => s.ChangedAt)
            .IsRequired();

        builder.Property(s => s.Notes)
            .HasMaxLength(255);

        builder.Property(s => s.CreatedAt)
            .IsRequired();
    }
}
