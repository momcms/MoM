using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MoM.Module.Interfaces;
using MoM.Module.Services;
using Microsoft.AspNetCore.Identity;
using MoM.Module.Models;
using MoM.Module.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoM.Web.Controllers.Api
{
    [Route("api/[controller]")]
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly IIdentityService Service;
        private readonly IEmailSender EmailSender;
        private readonly ISmsSender SmsSender;
        private readonly ILogger Logger;

        public IdentityController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            ILoggerFactory loggerFactory
            )
        {
            UserManager = userManager;
            Logger = loggerFactory.CreateLogger<AccountController>();
            Service = new IdentityService(UserManager, roleManager, Logger);
            SignInManager = signInManager;
            EmailSender = emailSender;
            SmsSender = smsSender;
            
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

        [HttpPost("{roleName}")]
        [Route("createrole")]
        public void CreateRole (string roleName)
        {
            Service.CreateRole(roleName);
        }
    }
}
