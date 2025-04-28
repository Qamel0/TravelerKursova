using System;
using System.Collections.Generic;

namespace Traveler.Models.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public ICollection<Stay>? Stays { get; set; }
}
