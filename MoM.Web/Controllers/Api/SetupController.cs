using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MoM.Module.Enums;
using MoM.Module.Dtos;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class SetupController : Controller
    {
        IOptions<SiteSettingDto> SiteSetting;
        private IConfiguration Configuration { get; set; }
        IHostingEnvironment Host;
        private InstallationStatus InstallStatus { get; set; }

        public SetupController(
            IOptions<SiteSettingDto> siteSetting, 
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
        [Route("isinstalled")]
        public SiteSettingDto IsInstalled()
        {
            if(SiteSetting.Value.IsInstalled)
            {
                return SiteSetting.Value;
            }
            else
            {
                return new SiteSettingDto { IsInstalled=false };
            }            
        }

        [HttpGet]
        [Route("getconnectionstring")]
        public SqlConnectionStringBuilder GetConnectionString()
        {
            var filepath = Host.ContentRootPath + "\\" + "appsettings.json";
            var appsettingsFile = JsonConvert.DeserializeObject<dynamic>(System.IO.File.ReadAllText(filepath));
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            if (appsettingsFile.InstallStatusMoM != InstallationStatus.Installed)
            {
                builder = new SqlConnectionStringBuilder(appsettingsFile.ConnectionStrings.DefaultConnection.Value);
            }
            
            return builder;
        }

        [HttpPost]
        [Route("saveconnectionstring")]
        public SiteSettingInstallationStatusDto SaveConnectionstring([FromBody] SiteSettingConnectionStringDto connectionstring)
        {
            if (InstallStatus == InstallationStatus.MissingConnectionString)
            {
                var filepath = Host.ContentRootPath + "\\" + "appsettings.json";
                var appsettingsFile = JsonConvert.DeserializeObject<dynamic>(System.IO.File.ReadAllText(filepath));
                appsettingsFile.ConnectionStrings.DefaultConnection = connectionstring.Value;
                appsettingsFile.InstallStatusMoM = "1";
                System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(appsettingsFile));

                CheckDatabaseConnection();
                //Todo apply migrations to the database
                return new SiteSettingInstallationStatusDto {
                    CompletedSteps = new int[] { 1 },
                    Message = "The connectionstring to the database have been saved",
                    InstallationResultCode = Result.Success.ToString()};
            }
            return new SiteSettingInstallationStatusDto {
                Message = "ConnectionString have allready been setup so you will need to reconfigure either using the admin interface or by manually editing appsettings.json",
                InstallationResultCode = Result.Warning.ToString()
            } ;
        }

        [HttpGet]
        [Route("checkdatabaseconnection")]
        public SiteSettingInstallationStatusDto CheckDatabaseConnection()
        {
            return new SiteSettingInstallationStatusDto
            {
                Message = "The connection is working",
                InstallationResultCode = Result.Success.ToString()
            };
        }

    }
}
