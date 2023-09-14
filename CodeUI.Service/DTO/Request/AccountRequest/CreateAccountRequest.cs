using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeUI.Service.DTO.Request.ProfileRequest;

namespace CodeUI.Service.DTO.Request.AccountRequest
{
    public class CreateAccountRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }

        public virtual CreateProfileRequest? CreateProfile { get; set; }
    }
}
