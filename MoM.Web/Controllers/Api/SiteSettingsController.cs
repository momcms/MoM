using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoM.Module.Config;
using MoM.Module.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class SiteSettingsController : Controller
    {
        IOptions<SiteSetting> SiteSetting;
        IHostingEnvironment Host;

        public SiteSettingsController(IOptions<SiteSetting> siteSetting, IHostingEnvironment host)
        {
            SiteSetting = siteSetting;
            Host = host;
        }
        // GET: api/values
        [HttpGet]
        [Route("getsitesettings")]
        public SiteSetting Get()
        {
            return SiteSetting.Value;
        }

        [HttpPost]
        [Route("savesitesettings")]
        public SiteSetting saveSiteSettings([FromBody] SiteSetting siteSettings)
        {
            //TODO Save the json then alter the configurationBuilder and return the updated object
            //var saveObj = siteSettings.ToJson();
            return siteSettings;
        }

        [HttpGet]
        [Route("getmodulepath")]
        public string GetModulePath()
        {
            return Host.ContentRootPath;
        }
    }
}
