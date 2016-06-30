using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoM.Module.Config;
using MoM.Module.Dtos;
using MoM.Module.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers
{
    public class ErrorController : Controller
    {
        IOptions<SiteSettingDto> SiteSetting;

        public ErrorController(IOptions<SiteSettingDto> siteSetting)
        {
            SiteSetting = siteSetting;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var theme = SiteSetting.Value.theme;
            ViewData["CssPath"] = "css/" + theme.module + "/" + theme.name + "/";
            return View();
        }
    }
}
