using System;
using System.Collections.Generic;

namespace cs.api.charityscan.Entities;

public partial class Volunteer
{
    public int Id { get; set; }

    public int AthleteId { get; set; }

    public int EventId { get; set; }

    public virtual Athlete Athlete { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
