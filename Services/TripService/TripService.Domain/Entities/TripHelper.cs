using Shared.Kernel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripService.Domain.Entities;

public class TripHelper : BaseEntity
{
    public Guid TripId { get; set; }
    public string HelperName { get; set; } = string.Empty;
    public decimal Pay { get; set; }

    public Trip Trip { get; set; } = null!;
}