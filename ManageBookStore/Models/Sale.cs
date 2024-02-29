using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class Sale
{
    public int Id { get; set; }

    public int? ProductId { get; set; }

    public double? NewPrice { get; set; }

    public DateTime? TimeStart { get; set; }

    public DateTime? TimeEnd { get; set; }

    public int? Quantity { get; set; }

    public bool? IsActive { get; set; }

    public int? MinQty { get; set; }

    public int? MaxQty { get; set; }

    public int? CreatedBy { get; set; }

    public virtual Account? CreatedByNavigation { get; set; }

    public virtual Book? Product { get; set; }
}
