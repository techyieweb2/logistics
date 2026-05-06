using DriverService.Domain.Enums;
using Shared.Kernel.Common;

namespace DriverService.Domain.Entities
{
    public class DriverDocument : BaseEntity
    {
        public Guid DriverId { get; set; }
        public string? DocumentType { get; set; }
        public string? FilePath { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public Driver Driver { get; set; } = null!;
    }
}
