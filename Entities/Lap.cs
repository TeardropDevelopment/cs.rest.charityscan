using System;
using System.Collections.Generic;

namespace cs.api.charityscan.Entities;

public partial class Lap
{
    public int Id { get; set; }

    public int? AthleteId { get; set; }

    public int EventId { get; set; }

    public int? StarterNr { get; set; }

    public virtual Athlete? Athlete { get; set; }

    public virtual Event Event { get; set; } = null!;
}
