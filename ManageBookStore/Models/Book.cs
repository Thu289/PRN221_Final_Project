using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class Book
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public bool? IsInStock { get; set; }

    public double? Price { get; set; }

    public string? Status { get; set; }

    public string? Image { get; set; }

    public double? Rating { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ProductAcc? ProductAcc { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
