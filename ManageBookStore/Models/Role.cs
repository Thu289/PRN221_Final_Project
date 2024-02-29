using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Account> Accs { get; set; } = new List<Account>();
}
