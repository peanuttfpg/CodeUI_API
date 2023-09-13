using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class Rating
{
    public int Id { get; set; }

    public int? Value { get; set; }

    public byte[]? Timestamp { get; set; }

    public int? AccountId { get; set; }

    public int? ElementId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Element? Element { get; set; }
}
