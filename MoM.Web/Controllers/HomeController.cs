using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoM.Module.Dtos;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers
{
    public class HomeController : Controller
    {
        IOptions<SiteSettingDto> SiteSetting;

        public HomeController(IOptions<SiteSettingDto> siteSetting)
        {
            SiteSetting = siteSetting;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var theme = SiteSetting.Value.theme;
            ViewData["CssPath"] = "css/" + theme.module + "/" + theme.name + "/";
            if (SiteSetting.Value.isInstalled)
            {
                return View("~/Views/MoM.CMS/App/Index.cshtml");
            }
            return View("~/Views/MoM.Setup/App/Index.cshtml");            
        }
    }
}
