using MoM.Module.Interfaces;
using System.Collections.Generic;
using System.Linq;
using MoM.Module.Models;
using MoM.Module.Dtos;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MoM.Module.Services
{
    [Authorize]
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly ILogger Logger;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Logger = logger;
        }

        public IEnumerable<UserDto> GetUsers(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            switch (sortColumn.ToLower())
            {
                case "username":
                    if (sortByAscending)
                        return UserManager.Users
                            .OrderBy(p => p.UserName)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize).ToDTOs();
                    else
                        return UserManager.Users
                            .OrderByDescending(p => p.UserName)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize).ToDTOs();
                case "email":
                    if (sortByAscending)
                        return UserManager.Users
                            .OrderBy(p => p.Email)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize).ToDTOs();
                    else
                        return UserManager.Users
                            .OrderByDescending(p => p.Email)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize).ToDTOs();
                default:
                    return UserManager.Users
                            .OrderByDescending(p => p.UserName)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize).ToDTOs();
            }
        }

        public async Task CreateUser(UserDto user, string password)
        {
            var result = await UserManager.CreateAsync(user.ToEntity(), password);
            if (!result.Succeeded)
            {
                Logger.LogWarning(result.Errors.ToString());
            }
        }

        public async Task UpdateUser(UserDto user)
        {
            var result = await UserManager.UpdateAsync(user.ToEntity());
            if (!result.Succeeded)
            {
                Logger.LogWarning(result.Errors.ToString());
            }
        }

        public async Task DeleteUser(UserDto user)
        {
            var result = await UserManager.DeleteAsync(user.ToEntity());
            if (!result.Succeeded)
            {
                Logger.LogWarning(result.Errors.ToString());
            }
        }

        public IEnumerable<RoleDto> GetRoles(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            switch (sortColumn)
            {
                default:
                    return RoleManager.Roles
                            .OrderByDescending(p => p.Name)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize).ToDTOs();
            }
        }

        public async Task CreateRole(string roleName)
        {
            var result = await RoleManager.CreateAsync(new IdentityRole { Name=roleName, NormalizedName = roleName.ToUpper(), ConcurrencyStamp= new Guid().ToString() });
            if (!result.Succeeded)
            {
                var error = result.Errors;
            }
        }

        public async Task UpdateRole(RoleDto role)
        {
            var result = await RoleManager.UpdateAsync(role.ToEntity());
            if (!result.Succeeded)
            {
                Logger.LogWarning(result.Errors.ToString());
            }
        }

        public async Task DeleteRole(RoleDto role)
        {
            var result = await RoleManager.DeleteAsync(role.ToEntity());
            if (!result.Succeeded)
            {
                Logger.LogWarning(result.Errors.ToString());
            }
        }

        public async Task<bool> AdminExist()
        {
            var admins = await UserManager.GetUsersInRoleAsync("Administrator");
            return admins.Count() > 0;
        }

        public async Task<RoleDto> GetRole(string roleName)
        {
            var role = await RoleManager.FindByNameAsync(roleName);
            return role.ToDTO();
        }
    }
}
