using DispatcherService.Domain.Enums;
using Shared.Kernel.Common;

namespace DispatcherService.Domain.Entities;

public class DispatchStatusHistory : BaseEntity
{
    public Guid DispatchId { get; set; }
    public DispatchStatus Status { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public string? Remarks { get; set; }

    public Dispatch Dispatch { get; set; } = null!;
}