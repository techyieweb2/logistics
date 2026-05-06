using Shared.Kernel.Common;
using TripService.Domain.Enums;

namespace TripService.Domain.Entities;

public class Trip : BaseEntity
{
    public Guid DispatchId { get; set; }            // External (Dispatcher Service)
    public decimal FreightCost { get; set; }
    public decimal DriverPay { get; set; }          // Pre-filled from Driver.FixRate, editable
    public TripStatus Status { get; set; } = TripStatus.Open;
    public string? Notes { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Denormalized from DispatcherService at creation time
    public Guid TruckId { get; set; }
    public string TruckPlateNumber { get; set; } = string.Empty;

    // Denormalized from DriverService at creation time
    public Guid DriverId { get; set; }
    public string DriverName { get; set; } = string.Empty;

    // Denormalized from CustomerService at creation time
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;

    // Denormalized from DispatcherService at creation time
    public string RouteName { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;

    // Navigation
    public ICollection<TripExpense> Expenses { get; set; } = [];
    public ICollection<TripHelper> Helpers { get; set; } = [];
    public ICollection<TripFuelExpense> FuelExpenses { get; set; } = [];
    public ICollection<TripSparePart> SpareParts { get; set; } = [];
}