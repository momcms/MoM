using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MoM.Module.Interfaces;
using MoM.Module.Services;
using Microsoft.AspNet.Identity;
using MoM.Module.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IIdentityService Service;

        [HttpGet("{pageNo}/{pageSize}/{sortColumn}/sortByAscending")]
        [Route("users")]
        public IEnumerable<ApplicationUser> GetUsers(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            return UserManager.Users;
        }
    }
}
