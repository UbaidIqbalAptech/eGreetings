﻿using System;
using System.Collections.Generic;

namespace eGreetings.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }
}
