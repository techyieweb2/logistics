using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruckService.Domain.Entities;

namespace TruckService.Persistence.Configurations
{
    public class TruckTrailerConfiguration : IEntityTypeConfiguration<TruckTrailer>
    {
        public void Configure(EntityTypeBuilder<TruckTrailer> builder)
        {
            builder.ToTable("TruckTrailers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.TruckId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.TrailerNumber)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasIndex(x => x.TrailerNumber)
                .IsUnique();

            // Flatbed, Curtainsider, Refrigerated, Container
            builder.Property(x => x.TrailerType)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.Capacity)
                .HasColumnType("nvarchar(50)");

            builder.Property(x => x.Status)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt)
                .HasColumnType("datetime2");
        }
    }
}