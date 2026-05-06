using TruckService.Domain.Enums;

namespace TruckService.Domain.Entities;

public class TruckTrailer
{
    public Guid Id { get; set; }
    public Guid TruckId { get; set; }
    public string TrailerNumber { get; set; } = string.Empty;

    /// <summary>Flatbed, Curtainsider, Refrigerated, Container</summary>
    public string? TrailerType { get; set; }

    public string? Capacity { get; set; }

    /// <summary>0=Inactive 1=Available 2=In-Use 3=Under Maintenance</summary>
    public TrailerStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public Truck? Truck { get; set; }
}