using DispatcherService.Domain.Enums;
using Shared.Kernel.Common;

namespace DispatcherService.Domain.Entities;

public class Dispatch : BaseEntity
{
    public string ReferenceNumber { get; set; } = string.Empty;

    // External references — no FK constraints
    public Guid DriverId { get; set; }
    public Guid TruckId { get; set; }

    public Guid RouteId { get; set; }
    public DispatchStatus Status { get; set; } = DispatchStatus.Pending;
    public DateTime ScheduledDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public string? Notes { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid CustomerId { get; set; }

    // Navigation
    public Route Route { get; set; } = null!;
    public ICollection<DispatchStatusHistory> StatusHistories { get; set; } = [];
    public ICollection<DispatchDocument> Documents { get; set; } = [];
}