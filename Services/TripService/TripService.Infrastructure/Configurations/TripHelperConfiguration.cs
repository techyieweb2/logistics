using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripService.Domain.Entities;

namespace TripService.Infrastructure.Configurations;

public class TripHelperConfiguration : IEntityTypeConfiguration<TripHelper>
{
    public void Configure(EntityTypeBuilder<TripHelper> builder)
    {
        builder.ToTable("TripHelpers");

        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).ValueGeneratedNever();

        builder.Property(h => h.HelperName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(h => h.Pay)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(h => h.CreatedAt)
            .IsRequired();
    }
}