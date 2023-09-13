using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class AccountChat
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public int? ChatBoxId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ChatBox? ChatBox { get; set; }
}
