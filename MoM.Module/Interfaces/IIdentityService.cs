using MoM.Module.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoM.Module.Interfaces
{
    public interface IIdentityService
    {
        IEnumerable<UserDto> GetUsers(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
        Task CreateUser(UserDto user, string password);
        Task UpdateUser(UserDto user);
        Task DeleteUser(UserDto user);

        IEnumerable<RoleDto> GetRoles(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
        Task CreateRole(string roleName);
        Task UpdateRole(RoleDto role);
        Task DeleteRole(RoleDto role);
    }
}
