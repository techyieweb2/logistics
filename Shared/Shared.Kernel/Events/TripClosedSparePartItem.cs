using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kernel.Events
{
    public sealed record TripClosedSparePartItem(
        string PartName,
        int Quantity,
        decimal UnitCost,
        decimal TotalCost
    );
}
