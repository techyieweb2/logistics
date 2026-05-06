using DriverService.Domain.Entities;
using DriverService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Infrastructure.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedNever();

        builder.Property(d => d.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.MiddleName)
            .HasMaxLength(100);

        builder.Property(d => d.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Gender)
            .HasMaxLength(10);

        builder.Property(d => d.DateOfBirth);

        builder.Property(d => d.Height)
            .HasMaxLength(20);

        builder.Property(d => d.Weight)
            .HasMaxLength(20);

        builder.Property(d => d.Religion)
            .HasMaxLength(50);

        builder.Property(d => d.CivilStatus)
            .HasMaxLength(30);

        builder.Property(d => d.CivilStatusPlace)
            .HasMaxLength(150);

        builder.Property(d => d.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(d => d.Email)
            .HasMaxLength(150);

        builder.Property(d => d.LicenseNumber)
            .HasMaxLength(50);

        builder.HasIndex(d => d.LicenseNumber)
            .IsUnique();

        builder.Property(d => d.SssNumber)
            .HasMaxLength(30);

        builder.Property(d => d.PhilHealthNumber)
            .HasMaxLength(30);

        builder.Property(d => d.PagIbigNumber)
            .HasMaxLength(30);

        builder.Property(d => d.TinNumber)
            .HasMaxLength(30);

        builder.Property(d => d.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(d => d.DateHired);

        builder.Property(d => d.CreatedAt)
            .IsRequired();

        builder.Property(d => d.UpdatedAt);

        builder.Property(d => d.FixRate)
            .HasColumnType("decimal(18,2)");

        // Relationships
        builder.HasMany(d => d.Addresses)
            .WithOne(a => a.Driver)
            .HasForeignKey(a => a.DriverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.FamilyMembers)
            .WithOne(f => f.Driver)
            .HasForeignKey(f => f.DriverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.EmergencyContacts)
            .WithOne(e => e.Driver)
            .HasForeignKey(e => e.DriverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Educations)
            .WithOne(e => e.Driver)
            .HasForeignKey(e => e.DriverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.EmploymentRecords)
            .WithOne(e => e.Driver)
            .HasForeignKey(e => e.DriverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.CharacterReferences)
            .WithOne(c => c.Driver)
            .HasForeignKey(c => c.DriverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Documents)
            .WithOne(d => d.Driver)
            .HasForeignKey(d => d.DriverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Assignments)
            .WithOne(a => a.Driver)
            .HasForeignKey(a => a.DriverId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.StatusHistories)
            .WithOne(s => s.Driver)
            .HasForeignKey(s => s.DriverId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
