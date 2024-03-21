using System;
using System.Collections.Generic;

namespace test.Models;

public partial class EnableUser
{
    public string Uid { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Type { get; set; }

    public string Password { get; set; } = null!;
}
