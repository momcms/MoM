using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoM.Module.Dtos
{
    public partial class UserDto
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public bool emailConfirmed { get; set; }
        public int accessFailedCount { get; set; }
        public string concurrencyStamp { get; set; }
        public bool lockoutEnabled { get; set; }
        public DateTimeOffset? lockoutEnd { get; set; }
        public string phoneNumber { get; set; }
        public bool phoneNumberConfirmed { get; set; }
        public bool twoFactorEnabled { get; set; }
        public List<RoleDto> roles { get; set; }
    }
}
