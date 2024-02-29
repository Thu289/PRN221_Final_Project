using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class Account
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? FullName { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Status { get; set; }

    public string? Avatar { get; set; }

    public bool? IsActive { get; set; }

    public string? RemoteIp { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<OrderCartImport> OrderCartImports { get; set; } = new List<OrderCartImport>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ProductAcc? ProductAcc { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Report> ReportReportedAccs { get; set; } = new List<Report>();

    public virtual ICollection<Report> ReportRepoters { get; set; } = new List<Report>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
