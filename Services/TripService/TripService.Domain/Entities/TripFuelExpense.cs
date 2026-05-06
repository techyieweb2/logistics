using Microsoft.VisualBasic.FileIO;
using Shared.Kernel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripService.Domain.Enums;

namespace TripService.Domain.Entities;

public class TripFuelExpense : BaseEntity
{
    public Guid TripId { get; set; }
    public FuelType FuelType { get; set; }
    public decimal Liters { get; set; }
    public decimal PricePerLiter { get; set; }
    public decimal Amount { get; set; }             // Auto-computed: Liters x PricePerLiter
    public string? Station { get; set; }

    public Trip Trip { get; set; } = null!;
}