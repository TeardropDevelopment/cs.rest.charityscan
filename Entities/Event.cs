using System;
using System.Collections.Generic;

namespace cs.api.charityscan.Entities;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Code> Codes { get; } = new List<Code>();

    public virtual EventCode? EventCode { get; set; }

    public virtual EventDetail? EventDetail { get; set; }

    public virtual ICollection<Lap> Laps { get; } = new List<Lap>();

    public virtual ICollection<Volunteer> Volunteers { get; } = new List<Volunteer>();
}
