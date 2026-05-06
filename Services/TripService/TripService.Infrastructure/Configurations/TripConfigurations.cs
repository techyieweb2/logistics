using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripService.Domain.Entities;

namespace TripService.Infrastructure.Configurations;

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.ToTable("Trips");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();

        builder.Property(t => t.DispatchId)
            .IsRequired();

        // Truck (denormalized)
        builder.Property(t => t.TruckId)
            .IsRequired();

        builder.Property(t => t.TruckPlateNumber)
            .IsRequired()
            .HasMaxLength(50);

        // Driver (denormalized)
        builder.Property(t => t.DriverId)
            .IsRequired();

        builder.Property(t => t.DriverName)
            .IsRequired()
            .HasMaxLength(200);

        // Customer (denormalized)
        builder.Property(t => t.CustomerId)
            .IsRequired();

        builder.Property(t => t.CustomerName)
            .IsRequired()
            .HasMaxLength(200);

        // Route (denormalized)
        builder.Property(t => t.RouteName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(t => t.Origin)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(t => t.Destination)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(t => t.FreightCost)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        // Driver pay is stored directly on Trip — pre-filled from FixRate, editable
        builder.Property(t => t.DriverPay)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(t => t.Notes)
            .HasMaxLength(500);

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.UpdatedAt);

        // Relationships
        builder.HasMany(t => t.Expenses)
            .WithOne(e => e.Trip)
            .HasForeignKey(e => e.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Helpers)
            .WithOne(h => h.Trip)
            .HasForeignKey(h => h.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.FuelExpenses)
            .WithOne(f => f.Trip)
            .HasForeignKey(f => f.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.SpareParts)
            .WithOne(s => s.Trip)
            .HasForeignKey(s => s.TripId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}