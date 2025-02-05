using System;
using System.Collections.Generic;

namespace dotNetProject.Models;

public partial class Township
{
    public string TownshipId { get; set; } = null!;

    public string? TownshipName { get; set; }

    public double? Latitude { get; set; }
}
