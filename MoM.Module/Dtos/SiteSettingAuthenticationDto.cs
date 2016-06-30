namespace MoM.Module.Dtos
{
    public class SiteSettingAuthenticationDto
    {
        public SiteSettingAuthenticationFacebookDto facebook { get; set; }
        public SiteSettingAuthenticationGoogleDto google { get; set; }
        public SiteSettingAuthenticationMicrosoftDto microsoft { get; set; }
        public SiteSettingAuthenticationTwitterDto twitter { get; set; }
    }
}
