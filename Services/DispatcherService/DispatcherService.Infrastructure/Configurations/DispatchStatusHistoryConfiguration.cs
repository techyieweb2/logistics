using DispatcherService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DispatcherService.Infrastructure.Configurations;

public class DispatchStatusHistoryConfiguration : IEntityTypeConfiguration<DispatchStatusHistory>
{
    public void Configure(EntityTypeBuilder<DispatchStatusHistory> builder)
    {
        builder.ToTable("DispatchStatusHistories");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();

        builder.Property(s => s.DispatchId)
            .IsRequired();

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(s => s.ChangedAt)
            .IsRequired();

        builder.Property(s => s.Remarks)
            .HasMaxLength(255);

        builder.Property(s => s.CreatedAt)
            .IsRequired();
    }
}
