using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Infrastructure.Configurations;

public class DriverEducationConfiguration : IEntityTypeConfiguration<DriverEducation>
{
    public void Configure(EntityTypeBuilder<DriverEducation> builder)
    {
        builder.ToTable("DriverEducations");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Level)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(e => e.SchoolName)
            .HasMaxLength(200);

        builder.Property(e => e.YearFrom);

        builder.Property(e => e.YearTo);

        builder.Property(e => e.CreatedAt)
            .IsRequired();
    }
}
