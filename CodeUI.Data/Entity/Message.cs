using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class Message
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public string? Content { get; set; }

    public byte[]? Timestamp { get; set; }

    public string? Status { get; set; }

    public int? ChatBoxId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ChatBox? ChatBox { get; set; }
}
