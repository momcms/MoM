namespace MoM.Module.Dtos
{
    public class SiteSettingEmailDto
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
