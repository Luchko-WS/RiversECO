using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace RiversECO.Models
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedOn { get; set; }
        public string City { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
