﻿using System;
using System.Collections.Generic;

namespace HomestayWeb.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public bool? Gender { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
