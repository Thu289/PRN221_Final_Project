﻿using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Book> Products { get; set; } = new List<Book>();
}