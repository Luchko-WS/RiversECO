using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace RiversECO.Models
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
