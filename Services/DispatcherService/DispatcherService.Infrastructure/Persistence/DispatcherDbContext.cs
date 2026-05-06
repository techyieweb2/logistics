using DispatcherService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DispatcherService.Infrastructure.Persistence;

public class DispatcherDbContext : DbContext
{
    public DispatcherDbContext(DbContextOptions<DispatcherDbContext> options) : base(options) { }

    public DbSet<Dispatch> Dispatches => Set<Dispatch>();
    public DbSet<Route> Routes => Set<Route>();
    public DbSet<DispatchStatusHistory> DispatchStatusHistories => Set<DispatchStatusHistory>();
    public DbSet<DispatchDocument> DispatchDocuments => Set<DispatchDocument>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DispatcherDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}