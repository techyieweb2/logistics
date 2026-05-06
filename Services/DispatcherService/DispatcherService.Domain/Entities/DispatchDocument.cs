using DispatcherService.Domain.Enums;
using Shared.Kernel.Common;

namespace DispatcherService.Domain.Entities;

public class DispatchDocument : BaseEntity
{
    public Guid DispatchId { get; set; }
    public DocumentType? DocumentType { get; set; }
    public string? FilePath { get; set; }

    public Dispatch Dispatch { get; set; } = null!;
}