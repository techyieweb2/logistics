using DriverService.Domain.Enums;
using Shared.Kernel.Common;

namespace DriverService.Domain.Entities
{
    public class DriverEducation : BaseEntity
    {
        public Guid DriverId { get; set; }
        public EducationLevel Level { get; set; }
        public string? SchoolName { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }

        public Driver Driver { get; set; } = null!;
    }
}
