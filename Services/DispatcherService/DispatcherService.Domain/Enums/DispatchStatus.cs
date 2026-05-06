using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispatcherService.Domain.Enums
{
    public enum DispatchStatus
    {
        Pending = 0,
        InTransit = 1,
        Completed = 2,
        Cancelled = 3
    }
}
