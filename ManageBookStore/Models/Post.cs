using System;
using System.Collections.Generic;

namespace ManageBookStore.Models;

public partial class Post
{
    public int Id { get; set; }

    public int? Writer { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Contents { get; set; }

    public double? Rating { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Account? WriterNavigation { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
