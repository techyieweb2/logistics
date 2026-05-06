namespace TruckService.Domain.Entities;

public class TruckMaintenanceRecord
{
    public Guid Id { get; set; }
    public Guid TruckId { get; set; }

    /// <summary>Preventive, Corrective, Tire Change, Oil Change</summary>
    public string? MaintenanceType { get; set; }

    public string? Description { get; set; }
    public string? OdometerReading { get; set; }
    public DateTime ServiceDate { get; set; }
    public string? ServicedBy { get; set; }
    public decimal? Cost { get; set; }
    public DateTime? NextServiceDate { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public Truck? Truck { get; set; }
}