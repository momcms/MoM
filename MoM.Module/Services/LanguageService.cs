using MoM.Module.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoM.Module.Dtos;

namespace MoM.Module.Services
{
    public class LanguageService : ILanguageService
    {
        private IDataStorage Storage;

        public LanguageService(IDataStorage storage)
        {
            Storage = storage;
        }
        public IEnumerable<LanguageDto> GetLanguages()
        {
            return Storage.GetRepository<ILanguageRepository>().Table().ToDTOs(); 
        }
    }
}
