using DriverService.Domain.Enums;
using Shared.Kernel.Common;

namespace DriverService.Domain.Entities
{
    public class DriverFamilyMember : BaseEntity
    {
        public Guid DriverId { get; set; }
        public FamilyRelationship Relationship { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Occupation { get; set; }
        public string? ContactNumber { get; set; }

        public Driver Driver { get; set; } = null!;
    }
}
