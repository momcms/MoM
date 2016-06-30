namespace MoM.Module.Dtos
{
    public class SiteSettingEmailDto
    {
        public string hostName { get; set; }
        public string password { get; set; }
        public int port { get; set; }
        public bool requireCredentials { get; set; }
        public string senderEmailAdress { get; set; }
        public string userName { get; set; }
        public bool useSSL { get; set; }
    }
}
