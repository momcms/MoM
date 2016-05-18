using MoM.Module.Extensions;
using MoM.Module.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MoM.Module.Models.Seed
{
    public class LanguageSeed
    {

        private IDataStorage Storage;

        public LanguageSeed(IDataStorage storage)
        {
            Storage = storage;
        }

        public void Seed()
        {
            foreach (var language in LanguageList())
            {
                var exists = Storage.GetRepository<ILanguageRepository>().Fetch(x => x.Country.CultureLanguageName == language.Country.CultureLanguageName).FirstOrDefault();
                if (exists != null)
                {
                    language.LanguageId = exists.LanguageId;
                    if (exists != language) //If the record from the seed list is different than whats in the database - then we update the record in the database
                        Storage.GetRepository<ILanguageRepository>().Update(language);
                }
                else
                {
                    //the country is not present in the database så we add the language
                    Storage.GetRepository<ILanguageRepository>().Create(language);
                }
            }
        }
        public static IEnumerable<Language> LanguageList()
        {
            var culturelist = new List<string>
            {
                "en-US",
                "da-DK"
            };
            var countries = CountrySeed.CountryList.AsQueryable().WhereIn(x => x.CultureLanguageName, culturelist);
            var result = new List<Language>();
            foreach (var country in countries)
            {
                result.Add(new Language { Country = country });
            }
            return result;
        }
    }
}
