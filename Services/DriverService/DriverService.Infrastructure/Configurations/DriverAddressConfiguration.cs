using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Infrastructure.Configurations;

public class DriverAddressConfiguration : IEntityTypeConfiguration<DriverAddress>
{
    public void Configure(EntityTypeBuilder<DriverAddress> builder)
    {
        builder.ToTable("DriverAddresses");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.Property(a => a.AddressType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(a => a.Street)
            .HasMaxLength(255);

        builder.Property(a => a.Barangay)
            .HasMaxLength(100);

        builder.Property(a => a.City)
            .HasMaxLength(100);

        builder.Property(a => a.Province)
            .HasMaxLength(100);

        builder.Property(a => a.ZipCode)
            .HasMaxLength(10);

        builder.Property(a => a.IsPrimary)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(a => a.CreatedAt)
            .IsRequired();
    }
}
