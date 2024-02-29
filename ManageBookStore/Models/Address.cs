using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class Address
{
    public int Id { get; set; }

    public int? AccId { get; set; }

    public string? Address1 { get; set; }

    public bool? IsDefault { get; set; }

    public virtual Account? Acc { get; set; }

    public virtual ICollection<OrderCartImport> OrderCartImports { get; set; } = new List<OrderCartImport>();
}
