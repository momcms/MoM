namespace MoM.Module.Dtos
{
    public class SiteSettingDto
    {
        public string title { get; set; }
        public SiteSettingThemeDto theme { get; set; }
        public SiteSettingAuthenticationDto authentication { get; set; }
        public bool isInstalled { get; set; }
        public SiteSettingLogoDto logo { get; set; }
        public string modulePath { get; set; }
        public SiteSettingEmailDto email { get; set; }
    }
}
