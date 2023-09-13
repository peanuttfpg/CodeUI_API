using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class Element
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }

    public string? Target { get; set; }

    public string? Status { get; set; }

    public int? CategoryId { get; set; }

    public int? OwnerId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Collaboration> Collaborations { get; set; } = new List<Collaboration>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<LikeTable> LikeTables { get; set; } = new List<LikeTable>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Snippet> Snippets { get; set; } = new List<Snippet>();
}
