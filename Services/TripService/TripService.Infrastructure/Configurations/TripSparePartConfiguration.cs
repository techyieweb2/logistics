using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripService.Domain.Entities;

namespace TripService.Infrastructure.Configurations;

public class TripSparePartConfiguration : IEntityTypeConfiguration<TripSparePart>
{
    public void Configure(EntityTypeBuilder<TripSparePart> builder)
    {
        builder.ToTable("TripSpareParts");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();

        builder.Property(s => s.PartName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Quantity)
            .IsRequired();

        builder.Property(s => s.UnitCost)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.TotalCost)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.CreatedAt)
            .IsRequired();
    }
}