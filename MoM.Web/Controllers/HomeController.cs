using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoM.Module.Config;
using MoM.Module.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers
{
    public class HomeController : Controller
    {
        IOptions<SiteSetting> SiteSetting;

        public HomeController(IOptions<SiteSetting> siteSetting)
        {
            SiteSetting = siteSetting;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var theme = SiteSetting.Value.Theme;
            ViewData["CssPath"] = "css/" + theme.Module + "/" + theme.Name + "/";
            return View();
        }
    }
}
