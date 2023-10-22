﻿using System;
using System.Collections.Generic;

namespace HomestayWeb.Models;

public partial class Homestay
{
    public int HomestayId { get; set; }

    public string HomestayName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string Commune { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool Status { get; set; }

    public string Type { get; set; } = null!;

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
