using System;
using System.Collections.Generic;
using System.Text;

namespace MyTime.Shared.Model
{
    public class User : Entity // todo: probably inherit IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly MemberSinceDate { get; set; }
        public bool IsManager { get; set; }
        public bool IsAdmin { get; set; }
    }
}
