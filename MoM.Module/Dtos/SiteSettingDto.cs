namespace MoM.Module.Dtos
{
    public class SiteSettingDto
    {
        public string Title { get; set; }
        public SiteSettingThemeDto Theme { get; set; }
        public SiteSettingAuthenticationDto Authentication { get; set; }
        public bool IsInstalled { get; set; }
        public SiteSettingLogoDto Logo { get; set; }
        public string ModulePath { get; set; }
        public SiteSettingEmailDto Email { get; set; }
    }
}
