﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoM.Module.Dtos;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers
{
    public class PagesController : Controller
    {
        IOptions<SiteSettingDto> SiteSetting;

        public PagesController(IOptions<SiteSettingDto> siteSetting)
        {
            SiteSetting = siteSetting;
        }
        public IActionResult App() => PartialView();
    }
}
