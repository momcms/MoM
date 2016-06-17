namespace MoM.Module.Dtos
{
    public class SiteSettingAuthenticationDto
    {
        public SiteSettingAuthenticationFacebookDto Facebook { get; set; }
        public SiteSettingAuthenticationGoogleDto Google { get; set; }
        public SiteSettingAuthenticationMicrosoftDto Microsoft { get; set; }
        public SiteSettingAuthenticationTwitterDto Twitter { get; set; }
    }
}
