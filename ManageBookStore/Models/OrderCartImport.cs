using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class OrderCartImport
{
    public int Id { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? AccId { get; set; }

    public string? RemoteIp { get; set; }

    public string? Status { get; set; }

    public double? TotalPrice { get; set; }

    public double? ShippingFees { get; set; }

    public int? AddressId { get; set; }

    public double? Discount { get; set; }

    public double? FinalPrice { get; set; }

    public string? Type { get; set; }

    public virtual Account? Acc { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
