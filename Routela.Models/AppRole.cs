﻿using Microsoft.AspNetCore.Identity;

namespace Routela.Models
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
