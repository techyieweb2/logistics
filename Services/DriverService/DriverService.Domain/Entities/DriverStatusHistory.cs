using DriverService.Domain.Enums;
using Shared.Kernel.Common;

namespace DriverService.Domain.Entities
{
    public class DriverStatusHistory : BaseEntity
    {
        public Guid DriverId { get; set; }
        public DriverStatus Status { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public string? Notes { get; set; }

        public Driver Driver { get; set; } = null!;
    }
}
