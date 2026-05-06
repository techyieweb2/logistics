using Shared.Kernel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripService.Domain.Enums;

namespace TripService.Domain.Entities;

public class TripExpense : BaseEntity
{
    public Guid TripId { get; set; }
    public ExpenseType ExpenseType { get; set; }    // TollFee, Other only
    public string? Description { get; set; }
    public decimal Amount { get; set; }

    public Trip Trip { get; set; } = null!;
}