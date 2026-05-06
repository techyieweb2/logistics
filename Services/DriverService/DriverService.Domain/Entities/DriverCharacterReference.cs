using DriverService.Domain.Enums;
using Shared.Kernel.Common;

namespace DriverService.Domain.Entities
{
    public class DriverCharacterReference : BaseEntity
    {
        public Guid DriverId { get; set; }
        public string? FullName { get; set; }
        public string? Company { get; set; }
        public string? Position { get; set; }
        public string? ContactNumber { get; set; }

        public Driver Driver { get; set; } = null!;
    }
}
