using MoM.Module.Models;
using System.Collections.Generic;

namespace MoM.Web.Interfaces
{
    public interface IIdentityService
    {
        IEnumerable<ApplicationUser> GetUsers(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
    }
}
