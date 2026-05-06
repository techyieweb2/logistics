using DispatcherService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DispatcherService.Infrastructure.Configurations;

public class DispatchDocumentConfiguration : IEntityTypeConfiguration<DispatchDocument>
{
    public void Configure(EntityTypeBuilder<DispatchDocument> builder)
    {
        builder.ToTable("DispatchDocuments");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).ValueGeneratedNever();

        builder.Property(d => d.DispatchId)
            .IsRequired();

        builder.Property(d => d.DocumentType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(d => d.FilePath)
            .HasMaxLength(255);

        builder.Property(d => d.CreatedAt)
            .IsRequired();
    }
}
