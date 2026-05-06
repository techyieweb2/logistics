using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Infrastructure.Configurations;

public class DriverDocumentConfiguration : IEntityTypeConfiguration<DriverDocument>
{
    public void Configure(EntityTypeBuilder<DriverDocument> builder)
    {
        builder.ToTable("DriverDocuments");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedNever();

        builder.Property(d => d.DocumentType)
            .HasMaxLength(50);

        builder.Property(d => d.FilePath)
            .HasMaxLength(255);

        builder.Property(d => d.ExpiryDate);

        builder.Property(d => d.CreatedAt)
            .IsRequired();
    }
}
