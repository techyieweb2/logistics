using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruckService.Domain.Entities;

namespace TruckService.Persistence.Configurations
{
    public class TruckMaintenanceRecordConfiguration : IEntityTypeConfiguration<TruckMaintenanceRecord>
    {
        public void Configure(EntityTypeBuilder<TruckMaintenanceRecord> builder)
        {
            builder.ToTable("TruckMaintenanceRecords");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.TruckId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            // Preventive, Corrective, Tire Change, Oil Change
            builder.Property(x => x.MaintenanceType)
                .HasColumnType("nvarchar(100)");

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar(500)");

            builder.Property(x => x.OdometerReading)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.ServiceDate)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(x => x.ServicedBy)
                .HasColumnType("nvarchar(200)");

            builder.Property(x => x.Cost)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.NextServiceDate)
                .HasColumnType("datetime2");

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}