using DispatcherService.Domain.Entities;
using DispatcherService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DispatcherService.Infrastructure.Configurations;

public class DispatchConfiguration : IEntityTypeConfiguration<Dispatch>
{
    public void Configure(EntityTypeBuilder<Dispatch> builder)
    {
        builder.ToTable("Dispatches");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedNever();

        builder.Property(d => d.ReferenceNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(d => d.ReferenceNumber)
            .IsUnique();

        // External references — no FK constraints
        builder.Property(d => d.DriverId)
            .IsRequired();

        builder.Property(d => d.TruckId)
            .IsRequired();

        builder.Property(d => d.RouteId)
            .IsRequired();

        builder.Property(d => d.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(d => d.ScheduledDate)
            .IsRequired();

        builder.Property(d => d.DepartureDate);

        builder.Property(d => d.ArrivalDate);

        builder.Property(d => d.Notes)
            .HasMaxLength(500);

        builder.Property(d => d.CreatedAt)
            .IsRequired();

        builder.Property(d => d.UpdatedAt);

        builder.Property(d => d.CustomerId)
            .IsRequired();

        // Relationships
        builder.HasOne(d => d.Route)
            .WithMany(r => r.Dispatches)
            .HasForeignKey(d => d.RouteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.StatusHistories)
            .WithOne(s => s.Dispatch)
            .HasForeignKey(s => s.DispatchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Documents)
            .WithOne(doc => doc.Dispatch)
            .HasForeignKey(doc => doc.DispatchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
