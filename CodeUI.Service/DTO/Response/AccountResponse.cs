using CodeUI.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeUI.Service.DTO.Response
{
    public class AccountResponse
    {
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public bool? IsActive { get; set; }

        public int? ProfileId { get; set; }

        public ICollection<Follow>? FollowFollowers { get; set; }

        public ICollection<Follow>? FollowFollowings { get; set; }

        public Profile? Profile { get; set; }
    }
}
