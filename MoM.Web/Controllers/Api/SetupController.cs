using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoM.Module.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MoM.Module.Enums;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class SetupController : Controller
    {
        IOptions<SiteSetting> SiteSetting;
        private IConfiguration Configuration { get; set; }
        IHostingEnvironment Host;
        private InstallationStatus InstallStatus { get; set; }

        public SetupController(
            IOptions<SiteSetting> siteSetting, 
            IHostingEnvironment host, 
            IConfiguration configuration
            )
        {
            SiteSetting = siteSetting;
            Host = host;
            Configuration = configuration;
            InstallStatus = (InstallationStatus)int.Parse(configuration["InstallStatusMoM"]);
        }

        [HttpGet]
        [Route("getsitesettings")]
        public SiteSetting Get()
        {
            if(SiteSetting.Value.IsInstalled)
            {
                return null;
            }
            else
            {
                return SiteSetting.Value;
            }            
        }

        [HttpPost]
        [Route("saveconnectionstring")]
        public InstallationResult SaveConnectionstring([FromBody] ConnectionString connectionstring)
        {
            if (InstallStatus == InstallationStatus.MissingConnectionString)
            {
                var filepath = Host.ContentRootPath + "\\" + "appsettings.json";
                var appsettingsFile = JsonConvert.DeserializeObject<dynamic>(System.IO.File.ReadAllText(filepath));
                appsettingsFile.ConnectionStrings.DefaultConnection = connectionstring.Value;
                appsettingsFile.InstallStatusMoM = "1";
                System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(appsettingsFile));

                CheckDatabaseConnection(connectionstring);
                //Todo apply migrations to the database
                return new InstallationResult {
                    Message = "The connectionstring to the database have been saved",
                    InstallationResultCode = InstallationResultCode.Success};
            }
            return new InstallationResult {
                Message = "ConnectionString have allready been setup so you will need to reconfigure either using the admin interface or by manually editing appsettings.json",
                InstallationResultCode = InstallationResultCode.Warning } ;
        }

        [HttpPost]
        [Route("checkdatabaseconnection")]
        public bool CheckDatabaseConnection([FromBody] ConnectionString connectionstring)
        {
            return false;
        }

    }

    public class ConnectionString
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class InstallationResult
    {
        public InstallationResultCode InstallationResultCode { get; set; }
        public string Message { get; set; }
    }

    public enum InstallationResultCode
    {
        Success,
        Information,
        Warning,
        Error
    }
}
