namespace MoM.Module.Config
{
    public class SiteSettings
    {
        public string Title { get; set; }
        public Theme Theme { get; set; }
        public Authentication Authentication { get; set; }
        public bool IsInstalled { get; set; }
        public Logo Logo { get; set; }
        public string ModulePath { get; set; }
        public string ConnectionString { get; set; }
    }

    public class Theme
    {
        public string Module { get; set; }
        public string Selected { get; set; }
    }

    public class Authentication
    {
        public Facebook Facebook { get; set; }
        public Google Google { get; set; }
        public Microsoft Microsoft { get; set; }
    }

    public class Facebook
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public bool Enabled { get; set; }
    }

    public class Google
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool Enabled { get; set; }
    }

    public class Microsoft
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool Enabled { get; set; }
    }

    public class Twitter
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public bool Enabled { get; set; }
    }

    public class Logo
    {
        public int Height { get; set; }
        public string ImagePath { get; set; }
        public string SvgPath { get; set; }
        public bool UseImageLogo { get; set; }
        public bool UseSvgLogo { get; set; }
        public int Width { get; set; }
    }
}
