using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Infrastructure.Configurations;

public class DriverFamilyMemberConfiguration : IEntityTypeConfiguration<DriverFamilyMember>
{
    public void Configure(EntityTypeBuilder<DriverFamilyMember> builder)
    {
        builder.ToTable("DriverFamilyMembers");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedNever();

        builder.Property(f => f.Relationship)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(f => f.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(f => f.Occupation)
            .HasMaxLength(100);

        builder.Property(f => f.ContactNumber)
            .HasMaxLength(20);

        builder.Property(f => f.CreatedAt)
            .IsRequired();
    }
}
