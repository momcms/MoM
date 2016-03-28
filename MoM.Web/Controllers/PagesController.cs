using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers
{
    public class PagesController : Controller
    {
        public IActionResult App() => PartialView();

        public IActionResult Public() => PartialView();

        public IActionResult Home() => PartialView();

        public IActionResult Services() => PartialView();

        public IActionResult Admin() => PartialView();

        public IActionResult AdminContent() => PartialView();

        public IActionResult AdminSecurity() => PartialView();

        public IActionResult AdminSettings() => PartialView();
    }
}
