using Shared.Kernel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripService.Domain.Entities;

public class TripSparePart : BaseEntity
{
    public Guid TripId { get; set; }
    public string PartName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalCost { get; set; }          // Auto-computed: Quantity x UnitCost

    public Trip Trip { get; set; } = null!;
}