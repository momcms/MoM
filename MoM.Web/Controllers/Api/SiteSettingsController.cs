using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoM.Module.Config;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class SiteSettingsController : Controller
    {
        IOptions<SiteSettings> SiteSettings;

        public SiteSettingsController(IOptions<SiteSettings> siteSettings)
        {
            SiteSettings = siteSettings;
        }
        // GET: api/values
        [HttpGet]
        [Route("getsitesettings")]
        public SiteSettings Get()
        {
            return SiteSettings.Value;
        }

        [HttpPost]
        [Route("savesitesettings")]
        public SiteSettings saveSiteSettings([FromBody] SiteSettings siteSettings)
        {
            //TODO Save the json then alter the configurationBuilder and return the updated object
            //var saveObj = siteSettings.ToJson();
            return siteSettings;
        }
    }
}
