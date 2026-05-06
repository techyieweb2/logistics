using Shared.Kernel.Common;

namespace DispatcherService.Domain.Entities;

public class Route : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public int? EstimatedHours { get; set; }
    public decimal? DistanceKm { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public ICollection<Dispatch> Dispatches { get; set; } = [];
}