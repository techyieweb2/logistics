using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruckService.Domain.Entities;

namespace TruckService.Persistence.Configurations
{
    public class TruckDocumentConfiguration : IEntityTypeConfiguration<TruckDocument>
    {
        public void Configure(EntityTypeBuilder<TruckDocument> builder)
        {
            builder.ToTable("TruckDocuments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.TruckId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            // OR, CR, Insurance, Inspection, PMS
            builder.Property(x => x.DocumentType)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.FilePath)
                .HasColumnType("nvarchar(255)");

            builder.Property(x => x.ExpiryDate)
                .HasColumnType("datetime2");

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}