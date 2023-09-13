using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class Request
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public string? Status { get; set; }

    public double? Reward { get; set; }

    public int? CategoryId { get; set; }

    public int? CreateBy { get; set; }

    public int? ReceiveBy { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Account? CreateByNavigation { get; set; }

    public virtual Account? ReceiveByNavigation { get; set; }
}
