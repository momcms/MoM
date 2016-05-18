using MoM.Module.Interfaces;
using System.Collections.Generic;
using MoM.Module.Dtos;

namespace MoM.Module.Services
{
    public class CountryService : ICountryService
    {
        private IDataStorage Storage;

        public CountryService(IDataStorage storage)
        {
            Storage = storage;
        }

        public List<CountryDto> Countries()
        {
            return Storage.GetRepository<ICountryRepository>().Table().ToDTOs();
        }

        public IEnumerable<CountryDto> Countries(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            return Storage.GetRepository<ICountryRepository>().Countries(pageNo, pageSize, sortColumn, sortByAscending).ToDTOs();
        }

        public CountryDto Country(int countryId)
        {
            return Storage.GetRepository<ICountryRepository>().Country(countryId).ToDTO();
        }
    }
}
