using System;
using System.Collections.Generic;

namespace cs.api.charityscan.Entities;

public partial class EventDetail
{
    public int EventId { get; set; }

    public string? Description { get; set; }

    public float Distance { get; set; }

    public virtual Event Event { get; set; } = null!;
}
