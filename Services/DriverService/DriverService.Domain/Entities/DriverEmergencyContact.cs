using DriverService.Domain.Enums;
using Shared.Kernel.Common;

namespace DriverService.Domain.Entities
{
    public class DriverEmergencyContact : BaseEntity
    {
        public Guid DriverId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }

        public Driver Driver { get; set; } = null!;
    }
}
