using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;
using MoM.Web.Config;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers
{
    public class HomeController : Controller
    {
        IOptions<Site> SiteSettings;

        public HomeController(IOptions<Site> siteSettings)
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
