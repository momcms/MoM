using MoM.Module.Enums;

namespace MoM.Module.Dtos
{
    public class SiteSettingConnectionStringDto
    {
        public string server { get; set; }
        public string database { get; set; }
        public bool useWindowsAuthentication { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
