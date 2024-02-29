using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public double? Price { get; set; }

    public virtual OrderCartImport Order { get; set; } = null!;

    public virtual Book Product { get; set; } = null!;
}
