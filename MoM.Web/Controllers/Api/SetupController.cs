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
        [Route("api/setup/isinstalled")]
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
        [Route("api/setup/getconnectionstring")]
        public SiteSettingConnectionStringDto GetConnectionString()
        {
            var filepath = Host.ContentRootPath + "\\" + "appsettings.json";
            var appsettingsFile = JsonConvert.DeserializeObject<dynamic>(System.IO.File.ReadAllText(filepath));
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            if (InstallStatus != InstallationStatus.MissingConnectionString && InstallStatus != InstallationStatus.Installed)
            {
                builder = new SqlConnectionStringBuilder(appsettingsFile.ConnectionStrings.DefaultConnection.Value);
            }

            var result = new SiteSettingConnectionStringDto
            {
                database = builder.InitialCatalog,
                server = builder.DataSource,
                useWindowsAuthentication = builder.IntegratedSecurity,
                password = builder.Password,
                username = builder.UserID,
                installationStatus = appsettingsFile.InstallStatusMoM
            };
            
            return result;
        }

        [HttpGet]
        [Route("api/setup/checkdatabaseconnection")]
        public SiteSettingInstallationStatusDto CheckDatabaseConnection()
        {
            return new SiteSettingInstallationStatusDto
            {
                message = "The connection is working",
                installationResultCode = Result.Success.ToString()
            };
        }

        [HttpPost]
        [Route("api/setup/saveconnectionstring")]
        public SiteSettingInstallationStatusDto SaveConnectionstring([FromBody] SiteSettingConnectionStringDto connectionstring)
        {
            if(InstallStatus == InstallationStatus.Installed)
            {
                return new SiteSettingInstallationStatusDto
                {
                    completedSteps = new int[] { 1, 2, 3, 4, 5 },
                    message = "MoM is allready installed .Please use the admin interface ",
                    installationResultCode = Result.Warning.ToString()
                };
            }
            var filepath = Host.ContentRootPath + "\\" + "appsettings.json";
            var appsettingsFile = JsonConvert.DeserializeObject<dynamic>(System.IO.File.ReadAllText(filepath));
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = connectionstring.server;
            builder.InitialCatalog = connectionstring.database;
            builder.IntegratedSecurity = connectionstring.useWindowsAuthentication;
            if(!connectionstring.useWindowsAuthentication)
            {
                builder.Password = connectionstring.password;
                builder.UserID = connectionstring.username;
            }
            builder.MultipleActiveResultSets = true;
            appsettingsFile.ConnectionStrings.DefaultConnection = builder.ConnectionString;
            appsettingsFile.InstallStatusMoM = "1";
            System.IO.File.WriteAllText(filepath, JsonConvert.SerializeObject(appsettingsFile));

            CheckDatabaseConnection();
            //Todo apply migrations to the database
            return new SiteSettingInstallationStatusDto {
                completedSteps = new int[] { 1 },
                message = "The connectionstring to the database have been saved",
                installationResultCode = Result.Success.ToString()};

        }

        [HttpPost]
        [Route("api/setup/savesitesetting")]
        public SiteSettingInstallationStatusDto SaveSiteSetting(SiteSettingInputDto siteSetting)
        {
            return new SiteSettingInstallationStatusDto
            {
                message = "Sitesettings could not be saved. Most likely it is caused either because you have allready completed the installation or there is no connection to the database.",
                installationResultCode = Result.Error.ToString()
            };
        }
    }
}
