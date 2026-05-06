using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Infrastructure.Configurations;

public class DriverEmploymentRecordConfiguration : IEntityTypeConfiguration<DriverEmploymentRecord>
{
    public void Configure(EntityTypeBuilder<DriverEmploymentRecord> builder)
    {
        builder.ToTable("DriverEmploymentRecords");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.CompanyName)
            .HasMaxLength(200);

        builder.Property(e => e.Position)
            .HasMaxLength(100);

        builder.Property(e => e.YearFrom);

        builder.Property(e => e.YearTo);

        builder.Property(e => e.CreatedAt)
            .IsRequired();
    }
}
