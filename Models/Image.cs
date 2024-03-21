using System;
using System.Collections.Generic;

namespace test.Models;

public partial class Image
{
    public string ImageId { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Pid { get; set; } = null!;
}
