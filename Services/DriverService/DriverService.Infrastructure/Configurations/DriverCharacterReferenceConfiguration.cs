using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Infrastructure.Configurations;

public class DriverCharacterReferenceConfiguration : IEntityTypeConfiguration<DriverCharacterReference>
{
    public void Configure(EntityTypeBuilder<DriverCharacterReference> builder)
    {
        builder.ToTable("DriverCharacterReferences");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.Property(c => c.FullName)
            .HasMaxLength(200);

        builder.Property(c => c.Company)
            .HasMaxLength(200);

        builder.Property(c => c.Position)
            .HasMaxLength(100);

        builder.Property(c => c.ContactNumber)
            .HasMaxLength(20);

        builder.Property(c => c.CreatedAt)
            .IsRequired();
    }
}
