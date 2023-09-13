using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class Notification
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public bool? IsRead { get; set; }

    public byte[]? Timestamp { get; set; }

    public int? AccountId { get; set; }

    public int? SourceId { get; set; }

    public string? Data { get; set; }

    public virtual Account? Account { get; set; }
}
