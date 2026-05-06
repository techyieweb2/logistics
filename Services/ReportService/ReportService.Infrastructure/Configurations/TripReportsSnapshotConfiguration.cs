using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportService.Domain.Entities;

namespace ReportService.Infrastructure.Configurations;

public class TripReportSnapshotConfiguration : IEntityTypeConfiguration<TripReportSnapshot>
{
    public void Configure(EntityTypeBuilder<TripReportSnapshot> builder)
    {
        builder.ToTable("TripReportSnapshots");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();

        // Unique per trip — idempotency guard
        builder.HasIndex(t => t.TripId).IsUnique();

        // Composite index for report queries by truck + period
        builder.HasIndex(t => new { t.TruckId, t.Month, t.Year });

        builder.Property(t => t.TripId).IsRequired();
        builder.Property(t => t.DispatchId).IsRequired();

        builder.Property(t => t.TruckId).IsRequired();
        builder.Property(t => t.TruckPlateNumber).IsRequired().HasMaxLength(50);

        builder.Property(t => t.DriverId).IsRequired();
        builder.Property(t => t.DriverName).IsRequired().HasMaxLength(200);

        builder.Property(t => t.CustomerId).IsRequired();
        builder.Property(t => t.CustomerName).IsRequired().HasMaxLength(200);

        builder.Property(t => t.RouteName).IsRequired().HasMaxLength(150);
        builder.Property(t => t.Origin).IsRequired().HasMaxLength(150);
        builder.Property(t => t.Destination).IsRequired().HasMaxLength(150);

        builder.Property(t => t.Month).IsRequired();
        builder.Property(t => t.Year).IsRequired();

        builder.Property(t => t.FreightCost).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(t => t.DriverPay).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(t => t.TotalHelperPay).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(t => t.TotalFuelCost).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(t => t.TotalTollFee).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(t => t.TotalOtherExpenses).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(t => t.TotalSparePartsCost).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(t => t.TotalExpenses).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(t => t.NetAmount).IsRequired().HasColumnType("decimal(18,2)");

        // JSON columns — nvarchar(max) to store unlimited line items
        builder.Property(t => t.ExpensesJson)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.HelpersJson)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.FuelExpensesJson)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.SparePartsJson)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.ClosedAt).IsRequired();
        builder.Property(t => t.CreatedAt).IsRequired();
    }
}