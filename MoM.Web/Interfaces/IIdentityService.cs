using MoM.Module.Dtos;
using System.Collections.Generic;

namespace MoM.Web.Interfaces
{
    public interface IIdentityService
    {
        IEnumerable<UserDto> GetUsers(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
        void CreateUser(UserDto user);
        void UpdateUser(UserDto user);
        void DeleteUser(UserDto user);

        IEnumerable<RoleDto> GetRoles(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
    }
}
