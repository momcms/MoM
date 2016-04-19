using System.Collections.Generic;

namespace MoM.Module.Dtos
{
    public partial class RoleDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string concurrencyStamp { get; set; }
        public string normalizedName { get; set; }
        public List<UserDto> users { get; set; }
    }
}
