using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripService.Domain.Entities;

namespace TripService.Infrastructure.Configurations;

public class TripFuelExpenseConfiguration : IEntityTypeConfiguration<TripFuelExpense>
{
    public void Configure(EntityTypeBuilder<TripFuelExpense> builder)
    {
        builder.ToTable("TripFuelExpenses");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedNever();

        builder.Property(f => f.FuelType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(f => f.Liters)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(f => f.PricePerLiter)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(f => f.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(f => f.Station)
            .HasMaxLength(150);

        builder.Property(f => f.CreatedAt)
            .IsRequired();
    }
}