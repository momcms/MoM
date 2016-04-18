using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace MoM.Module.Models
{
    public class ApplicationUser : IdentityUser
    {
        string id { get; set; }
        string userName { get; set; }
        string email { get; set; }
        bool emailConfirmed { get; set; }
        int accessFailedCount { get; set; }
        string concurrencyStamp { get; set; }
        bool lockoutEnabled { get; set; }
        DateTimeOffset lockoutEnd { get; set; }
        string phoneNumber { get; set; }
        bool phoneNumberConfirmed { get; set; }
        bool twoFactorEnabled { get; set; }        
        List<IdentityRole> roles { get; set; }
    }
}
