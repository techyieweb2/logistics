using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruckService.Domain.Entities;

namespace TruckService.Persistence.Configurations
{
    public class TruckStatusHistoryConfiguration : IEntityTypeConfiguration<TruckStatusHistory>
    {
        public void Configure(EntityTypeBuilder<TruckStatusHistory> builder)
        {
            builder.ToTable("TruckStatusHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.TruckId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.ChangedAt)
                .HasColumnType("datetime2")
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.Notes)
                .HasColumnType("nvarchar(255)");
        }
    }
}