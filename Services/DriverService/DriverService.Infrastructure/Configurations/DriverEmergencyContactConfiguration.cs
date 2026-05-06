using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Infrastructure.Configurations;

public class DriverEmergencyContactConfiguration : IEntityTypeConfiguration<DriverEmergencyContact>
{
    public void Configure(EntityTypeBuilder<DriverEmergencyContact> builder)
    {
        builder.ToTable("DriverEmergencyContacts");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Address)
            .HasMaxLength(255);

        builder.Property(e => e.MobileNumber)
            .HasMaxLength(20);

        builder.Property(e => e.CreatedAt)
            .IsRequired();
    }
}
