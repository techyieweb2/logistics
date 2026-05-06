using TruckService.Domain.Enums;

namespace TruckService.Domain.Entities;

public class TruckStatusHistory
{
    public Guid Id { get; set; }
    public Guid TruckId { get; set; }
    public TruckStatus Status { get; set; }
    public DateTime ChangedAt { get; set; }
    public string? Notes { get; set; }

    // Navigation
    public Truck? Truck { get; set; }
}