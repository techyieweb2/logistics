namespace TruckService.Domain.Entities;

public class TruckDocument
{
    public Guid Id { get; set; }
    public Guid TruckId { get; set; }

    /// <summary>OR, CR, Insurance, Inspection, PMS</summary>
    public string? DocumentType { get; set; }

    public string? FilePath { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public Truck? Truck { get; set; }
}