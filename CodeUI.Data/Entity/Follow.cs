using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class Follow
{
    public int Id { get; set; }

    public int? FollowerId { get; set; }

    public int? FollowingId { get; set; }

    public virtual Account? Follower { get; set; }

    public virtual Account? Following { get; set; }
}
