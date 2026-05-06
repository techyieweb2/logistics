using TruckService.Domain.Enums;

namespace TruckService.Domain.Entities;
public class Truck
{
    public Guid Id { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    public string? ChassisNumber { get; set; }
    public string? EngineNumber { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int? YearModel { get; set; }
    public string? Color { get; set; }

    /// <summary>e.g. "Trailer"</summary>
    public string? TruckType { get; set; }

    public string? GrossVehicleWeight { get; set; }
    public string? Capacity { get; set; }

    /// <summary>Diesel, Gasoline</summary>
    public string? FuelType { get; set; }

    /// <summary>0=Inactive 1=Available 2=In-Use 3=Under Maintenance 4=Retired</summary>
    public TruckStatus Status { get; set; }

    public DateTime? AcquisitionDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public ICollection<TruckDocument> Documents { get; set; } = new List<TruckDocument>();
    public ICollection<TruckMaintenanceRecord> MaintenanceRecords { get; set; } = new List<TruckMaintenanceRecord>();
    public ICollection<TruckStatusHistory> StatusHistories { get; set; } = new List<TruckStatusHistory>();
    public ICollection<TruckTrailer> Trailers { get; set; } = new List<TruckTrailer>();
}