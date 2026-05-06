using DriverService.Domain.Enums;
using Shared.Kernel.Common;

namespace DriverService.Domain.Entities
{
    public class DriverAssignment : BaseEntity
    {
        public Guid DriverId { get; set; }
        public Guid? TruckId { get; set; } // External ref (Truck Service)
        public DateTime AssignedDate { get; set; }
        public DateTime? UnassignedDate { get; set; }

        public Driver Driver { get; set; } = null!;
    }
}
