namespace MoM.Module.Dtos
{
    public class SiteSettingInstallationStatusDto
    {
        public string installationResultCode { get; set; }
        public string message { get; set; }
        public int[] completedSteps { get; set; }
    }
}
