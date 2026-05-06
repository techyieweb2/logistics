using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kernel.Events
{
    public sealed record TripClosedFuelItem(
        string FuelType,
        decimal Liters,
        decimal PricePerLiter,
        decimal Amount,
        string? Station
    );
}
