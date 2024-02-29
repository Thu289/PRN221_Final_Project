using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class Report
{
    public int Id { get; set; }

    public int? RepoterId { get; set; }

    public int? ReportedAccId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Reason { get; set; }

    public string? Status { get; set; }

    public virtual Account? ReportedAcc { get; set; }

    public virtual Account? Repoter { get; set; }
}
