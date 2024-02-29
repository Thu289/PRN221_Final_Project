using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class ProductAcc
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public int? AccId { get; set; }

    public string? Role { get; set; }

    public virtual Book Id1 { get; set; } = null!;

    public virtual Account IdNavigation { get; set; } = null!;
}
