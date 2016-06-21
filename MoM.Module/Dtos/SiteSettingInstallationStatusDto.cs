using MoM.Module.Enums;

namespace MoM.Module.Dtos
{
    public class SiteSettingInstallationStatusDto
    {
        public SiteSettingDto siteSetting { get; set; }
        public string installationResultCode { get; set; }
        public string message { get; set; }
        public InstallationStatus installationStatus { get; set; }
    }
}
