using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class Rating
{
    public int Id { get; set; }

    public int? ReviewedId { get; set; }

    public int? NumOfStar { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Contents { get; set; }

    public int? CreatedBy { get; set; }

    public virtual Account? CreatedByNavigation { get; set; }

    public virtual Book? Reviewed { get; set; }

    public virtual Post? ReviewedNavigation { get; set; }
}
