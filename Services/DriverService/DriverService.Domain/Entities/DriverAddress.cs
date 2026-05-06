using DriverService.Domain.Enums;
using Shared.Kernel.Common;

namespace DriverService.Domain.Entities
{
    public class DriverAddress : BaseEntity
    {
        public Guid DriverId { get; set; }
        public AddressType AddressType { get; set; }
        public string? Street { get; set; }
        public string? Barangay { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? ZipCode { get; set; }
        public bool IsPrimary { get; set; } = false;

        public Driver Driver { get; set; } = null!;
    }
}
