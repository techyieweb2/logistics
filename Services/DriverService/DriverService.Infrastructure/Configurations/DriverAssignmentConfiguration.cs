using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Infrastructure.Configurations;

public class DriverAssignmentConfiguration : IEntityTypeConfiguration<DriverAssignment>
{
    public void Configure(EntityTypeBuilder<DriverAssignment> builder)
    {
        builder.ToTable("DriverAssignments");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        // TruckId is external (Truck Service) — no FK constraint
        builder.Property(a => a.TruckId);

        builder.Property(a => a.AssignedDate)
            .IsRequired();

        builder.Property(a => a.UnassignedDate);

        builder.Property(a => a.CreatedAt)
            .IsRequired();
    }
}
