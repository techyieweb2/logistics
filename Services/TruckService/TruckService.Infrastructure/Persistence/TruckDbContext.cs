using TruckService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TruckService.Infrastructure.Persistence
{
    public class TruckDbContext : DbContext
    {
        public DbSet<Truck> Trucks => Set<Truck>();
        public DbSet<TruckDocument> TruckDocuments => Set<TruckDocument>();
        public DbSet<TruckMaintenanceRecord> TruckMaintenanceRecords => Set<TruckMaintenanceRecord>();
        public DbSet<TruckStatusHistory> TruckStatusHistories => Set<TruckStatusHistory>();
        public DbSet<TruckTrailer> TruckTrailers => Set<TruckTrailer>();

        public TruckDbContext(DbContextOptions<TruckDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TruckDbContext).Assembly);
        }
    }
}