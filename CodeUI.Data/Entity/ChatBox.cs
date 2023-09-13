using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class ChatBox
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? LastActivityDate { get; set; }

    public virtual ICollection<AccountChat> AccountChats { get; set; } = new List<AccountChat>();
}
