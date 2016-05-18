using MoM.Module.Dtos;
using System.Collections.Generic;

namespace MoM.Module.Interfaces
{
    public interface ICountryService
    {
        CountryDto Country(int countryId);
        List<CountryDto> Countries();
        IEnumerable<CountryDto> Countries(int pageNo, int pageSize, string sortColumn, bool sortByAscending);        
    }
}
