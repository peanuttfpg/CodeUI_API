using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class Snippet
{
    public int Id { get; set; }

    public int? ElementId { get; set; }

    public byte[]? Htmlcode { get; set; }

    public byte[]? Csscode { get; set; }

    public byte[]? Jscode { get; set; }

    public byte[]? Theme { get; set; }

    public virtual Element? Element { get; set; }
}
