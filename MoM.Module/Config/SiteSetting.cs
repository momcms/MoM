using MoM.Module.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoM.Module.Config
{
    public partial class SiteSetting
    {
        public string Title { get; set; }
        public Theme Theme { get; set; }
        public Authentication Authentication { get; set; }
        public bool IsInstalled { get; set; }
        public Logo Logo { get; set; }
        public string ModulePath { get; set; }
        public Email Email { get; set; }
    }

    public partial class Theme
    {
        public string Module { get; set; }
        public string Name { get; set; }
    }

    public partial class Authentication
    {
        public AuthenticationFacebook Facebook { get; set; }
        public AuthenticationGoogle Google { get; set; }
        public AuthenticationMicrosoft Microsoft { get; set; }
        public AuthenticationTwitter Twitter { get; set; }
    }

    public partial class AuthenticationFacebook
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public bool Enabled { get; set; }
    }

    public partial class AuthenticationGoogle
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool Enabled { get; set; }
    }

    public partial class AuthenticationMicrosoft
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool Enabled { get; set; }
    }

    public partial class AuthenticationTwitter
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public bool Enabled { get; set; }
    }

    public partial class Logo
    {
        public int Height { get; set; }
        public string ImagePath { get; set; }
        public string SvgPath { get; set; }
        public bool UseImageLogo { get; set; }
        public bool UseSvgLogo { get; set; }
        public int Width { get; set; }
    }

    public partial class Email
    {
        public string HostName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool RequireCredentials { get; set; }
        public string SenderEmailAdress { get; set; }
        public string UserName { get; set; }
        public bool UseSSL { get; set; }
    }
}
