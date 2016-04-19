using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MoM.Web.Interfaces;
using MoM.Web.Services;
using Microsoft.AspNet.Identity;
using MoM.Module.Models;
using MoM.Module.Dtos;
using Microsoft.AspNet.Identity.EntityFramework;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly IIdentityService Service;

        public IdentityController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            Service = new IdentityService(UserManager, roleManager);
        }

        [HttpGet("{pageNo}/{pageSize}/{sortColumn}/{sortByAscending}")]
        [Route("users")]
        public IEnumerable<UserDto> GetUsers(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            return Service.GetUsers(pageNo, pageSize, sortColumn, sortByAscending);
        }

        [HttpGet("{pageNo}/{pageSize}/{sortColumn}/{sortByAscending}")]
        [Route("roles")]
        public IEnumerable<RoleDto> GetRoles(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            return Service.GetRoles(pageNo, pageSize, sortColumn, sortByAscending);
        }
    }
}
