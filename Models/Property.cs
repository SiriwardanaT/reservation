using System;
using System.Collections.Generic;

namespace test.Models;

public partial class Property
{
    public string Pid { get; set; } = null!;

    public double Price { get; set; }

    public string Facilities { get; set; } = null!;

    public int CountRoom { get; set; }

    public string Landownerid { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int? Status { get; set; }

    public string? Reason { get; set; }
}
