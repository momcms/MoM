using MoM.Module.Dtos;
using System.Collections.Generic;

namespace MoM.Module.Interfaces
{
    public interface ILanguageService
    {
        IEnumerable<LanguageDto> GetLanguages();
    }
}
