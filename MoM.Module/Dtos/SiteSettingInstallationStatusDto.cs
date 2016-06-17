namespace MoM.Module.Dtos
{
    public class SiteSettingInstallationStatusDto
    {
        public string InstallationResultCode { get; set; }
        public string Message { get; set; }
        public int[] CompletedSteps { get; set; }
    }
}
