using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoM.Module.Config;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers
{
    public class PagesController : Controller
    {
        IOptions<SiteSetting> SiteSetting;

        public PagesController(IOptions<SiteSetting> siteSetting)
        {
            SiteSetting = siteSetting;
        }
        public IActionResult App() => PartialView();
        public IActionResult AppInstall()
        {
            if (SiteSetting.Value.IsInstalled)
            {
                return PartialView("~/Views/Pages/App.cshtml");
            }
            return PartialView("~/Views/Pages/AppInstall.cshtml");
        }
        public IActionResult Install() => PartialView("~/Views/Pages/Install.cshtml");
    }
}
