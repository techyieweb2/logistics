using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route = DispatcherService.Domain.Entities.Route;

namespace DispatcherService.Infrastructure.Configurations;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable("Routes");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(r => r.Origin)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(r => r.Destination)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(r => r.EstimatedHours);

        builder.Property(r => r.DistanceKm)
            .HasColumnType("decimal(10,2)");

        builder.Property(r => r.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(r => r.CreatedAt)
            .IsRequired();

        builder.Property(r => r.UpdatedAt);
    }
}
