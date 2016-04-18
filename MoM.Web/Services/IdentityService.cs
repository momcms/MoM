using MoM.Web.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MoM.Module.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity.EntityFramework;
using MoM.Module.Dtos;

namespace MoM.Web.Services
{
    [Authorize]
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public IEnumerable<ApplicationUser> GetUsers(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            switch (sortColumn)
            {
                case "UserName":
                    if (sortByAscending)
                        return UserManager.Users
                            .OrderBy(p => p.UserName)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
                    else
                        return UserManager.Users
                            .OrderByDescending(p => p.UserName)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
                case "Email":
                    if (sortByAscending)
                        return UserManager.Users
                            .OrderBy(p => p.Email)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
                    else
                        return UserManager.Users
                            .OrderByDescending(p => p.Email)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
                default:
                    return UserManager.Users
                            .OrderByDescending(p => p.UserName)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
            }
        }

        public void CreateUser(UserDto user)
        {

        }

        public void UpdateUser(UserDto user)
        {

        }

        public void DeleteUser(UserDto user)
        {

        }

        public IEnumerable<IdentityRole> GetRoles(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            switch (sortColumn)
            {
                default:
                    return RoleManager.Roles
                            .OrderByDescending(p => p.Name)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
            }
        }
    }
}
