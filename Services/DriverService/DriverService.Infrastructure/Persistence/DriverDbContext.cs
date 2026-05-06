using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Infrastructure.Persistence
{
    public class DriverDbContext : DbContext
    {
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<DriverAddress> DriverAddresses => Set<DriverAddress>();
        public DbSet<DriverAssignment> DriverAssignments => Set<DriverAssignment>();
        public DbSet<DriverCharacterReference> DriverCharacterReferences => Set<DriverCharacterReference>();
        public DbSet<DriverDocument> DriverDocuments => Set<DriverDocument>();
        public DbSet<DriverEmergencyContact> DriverEmergencyContacts => Set<DriverEmergencyContact>();
        public DbSet<DriverEmploymentRecord> DriverEmploymentRecords => Set<DriverEmploymentRecord>();
        public DbSet<DriverFamilyMember> DriverFamilyMembers => Set<DriverFamilyMember>();
        public DbSet<DriverStatusHistory> DriverStatusHistories => Set<DriverStatusHistory>();

        public DriverDbContext(DbContextOptions<DriverDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DriverDbContext).Assembly);
        }
    }
}