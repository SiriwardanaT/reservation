using System;
using System.Collections.Generic;

namespace test.Models;

public partial class ReservationRequest
{
    public string Rid { get; set; } = null!;

    public int NoOfStudents { get; set; }

    public int NoOfRooms { get; set; }

    public string Pid { get; set; } = null!;

    public string Uid { get; set; } = null!;

    public int Status { get; set; }
}
