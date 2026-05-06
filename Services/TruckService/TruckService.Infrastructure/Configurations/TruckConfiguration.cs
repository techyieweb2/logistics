using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;
using TruckService.Domain.Entities;

namespace TruckService.Persistence.Configurations
{
    public class TruckConfiguration : IEntityTypeConfiguration<Truck>
    {
        public void Configure(EntityTypeBuilder<Truck> builder)
        {
            builder.ToTable("Trucks");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.PlateNumber)
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.HasIndex(x => x.PlateNumber)
                .IsUnique();

            builder.Property(x => x.ChassisNumber)
                .HasColumnType("nvarchar(100)");

            builder.HasIndex(x => x.ChassisNumber)
                .IsUnique()
                .HasFilter("[ChassisNumber] IS NOT NULL");

            builder.Property(x => x.EngineNumber)
                .HasColumnType("nvarchar(100)");

            builder.Property(x => x.Brand)
                .HasColumnType("nvarchar(100)");

            builder.Property(x => x.Model)
                .HasColumnType("nvarchar(100)");

            builder.Property(x => x.YearModel)
                .HasColumnType("int");

            builder.Property(x => x.Color)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.TruckType)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.GrossVehicleWeight)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.Capacity)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.FuelType)
                .HasColumnType("nvarchar(30)");

            builder.Property(x => x.Status)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.AcquisitionDate)
                .HasColumnType("date");

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2");

            // Relationships
            builder.HasMany(x => x.Documents)
                .WithOne(x => x.Truck)
                .HasForeignKey(x => x.TruckId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.MaintenanceRecords)
                .WithOne(x => x.Truck)
                .HasForeignKey(x => x.TruckId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.StatusHistories)
                .WithOne(x => x.Truck)
                .HasForeignKey(x => x.TruckId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Trailers)
                .WithOne(x => x.Truck)
                .HasForeignKey(x => x.TruckId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}