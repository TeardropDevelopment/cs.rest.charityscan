using System;
using System.Collections.Generic;

namespace cs.api.charityscan.Entities;

public partial class Address
{
    public int EventId { get; set; }

    public string CountryCode { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Zipcode { get; set; } = null!;

    public string City { get; set; } = null!;

    public int HouseNumber { get; set; }

    public virtual Event Event { get; set; } = null!;
}
