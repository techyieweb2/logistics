using DriverService.Domain.Enums;
using Shared.Kernel.Common;

namespace DriverService.Domain.Entities
{
    public class DriverEmploymentRecord : BaseEntity
    {
        public Guid DriverId { get; set; }
        public string? CompanyName { get; set; }
        public string? Position { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }

        public Driver Driver { get; set; } = null!;
    }
}
