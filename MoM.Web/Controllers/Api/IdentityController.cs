using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MoM.Web.Interfaces;
using MoM.Web.Services;
using Microsoft.AspNet.Identity;
using MoM.Module.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IIdentityService Service;

        public IdentityController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            Service = new IdentityService(UserManager);
        }

        [HttpGet("{pageNo}/{pageSize}/{sortColumn}/{sortByAscending}")]
        [Route("users")]
        public IEnumerable<ApplicationUser> GetUsers(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            return Service.GetUsers(pageNo, pageSize, sortColumn, sortByAscending);
        }
    }
}
