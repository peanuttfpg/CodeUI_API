using System;
using System.Collections.Generic;

namespace CodeUI.Data.Entity;

public partial class Account
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsActive { get; set; }

    public int? ProfileId { get; set; }

    public virtual ICollection<AccountChat> AccountChats { get; set; } = new List<AccountChat>();

    public virtual ICollection<Collaboration> Collaborations { get; set; } = new List<Collaboration>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Follow> FollowFollowers { get; set; } = new List<Follow>();

    public virtual ICollection<Follow> FollowFollowings { get; set; } = new List<Follow>();

    public virtual ICollection<LikeTable> LikeTables { get; set; } = new List<LikeTable>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Profile? Profile { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Request> RequestCreateByNavigations { get; set; } = new List<Request>();

    public virtual ICollection<Request> RequestReceiveByNavigations { get; set; } = new List<Request>();

    public virtual Role? Role { get; set; }
}
