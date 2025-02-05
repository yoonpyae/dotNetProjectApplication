using System;
using System.Collections.Generic;

namespace dotNetProject.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Address { get; set; }
}
