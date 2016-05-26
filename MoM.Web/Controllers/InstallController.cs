using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoM.Module.Config;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers
{
    public class InstallController : Controller
    {
        IOptions<SiteSettings> SiteSettings;

        public InstallController(IOptions<SiteSettings> siteSettings)
        {
            SiteSettings = siteSettings;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var theme = SiteSettings.Value.Theme;
            ViewData["CssPath"] = "css/" + theme.Module + "/" + theme.Selected + "/";
            return View();
        }
    }
}
