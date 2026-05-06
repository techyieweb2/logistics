using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripService.Domain.Entities;

namespace TripService.Infrastructure.Persistence;

public class TripDbContext : DbContext
{
    public TripDbContext(DbContextOptions<TripDbContext> options) : base(options) { }

    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TripExpense> TripExpenses => Set<TripExpense>();
    public DbSet<TripHelper> TripHelpers => Set<TripHelper>();
    public DbSet<TripFuelExpense> TripFuelExpenses => Set<TripFuelExpense>();
    public DbSet<TripSparePart> TripSpareParts => Set<TripSparePart>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TripDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
