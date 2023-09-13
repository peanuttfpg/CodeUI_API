using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class Report
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public string? Type { get; set; }

    public byte[]? Timestamp { get; set; }

    public int? AccountId { get; set; }

    public int? ElementId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Element? Element { get; set; }
}
