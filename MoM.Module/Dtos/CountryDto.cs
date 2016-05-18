namespace MoM.Module.Dtos
{
    public partial class CountryDto
    {

        public int countryId { get; set; }
        public string name { get; set; }
        public string iSO31661Alpha2 { get; set; }
        public string iSO31661Alpha3 { get; set; }
        public int iSO31661Numeric { get; set; }
        public string currencyCode { get; set; }
        public int currencyCodeNumeric { get; set; }
        public string currencyFormat { get; set; }
        public string cultureName { get; set; }
        public string cultureLanguageName { get; set; }
        public string cultureCode { get; set; }

        public CountryDto()
        {

        }

        public CountryDto(int CountryId, string Name, string ISO31661Alpha2, string ISO31661Alpha3, int ISO31661Numeric, string CurrencyCode, int CurrencyCodeNumeric, string CurrencyFormat, string CultureName, string CultureLanguageName, string CultureCode)
        {
            countryId = CountryId;
            name = Name;
            iSO31661Alpha2 = ISO31661Alpha2;
            iSO31661Alpha3 = ISO31661Alpha3;
            iSO31661Numeric = ISO31661Numeric;
            currencyCode = CurrencyCode;
            currencyCodeNumeric = CurrencyCodeNumeric;
            currencyFormat = CurrencyFormat;
            cultureName = CultureName;
            cultureLanguageName = CultureLanguageName;
            cultureCode = CultureCode;
        }
    }
}
