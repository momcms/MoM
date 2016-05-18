namespace MoM.Module.Dtos
{
    public partial class LanguageDto
    {
        public int key { get; set; }
        public CountryDto country { get; set; }

        public LanguageDto() { }

        public LanguageDto(
            int Id,
            CountryDto Country
            )
        {
            key = Id;
            country = Country;
        }
    }
}
