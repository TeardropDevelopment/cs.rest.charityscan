using System;
using System.Collections.Generic;

namespace cs.api.charityscan.Entities;

public partial class EventCode
{
    public string Value { get; set; } = null!;

    public int EventId { get; set; }

    public virtual Event Event { get; set; } = null!;
}
