using MoM.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoM.Module.Dtos
{
    public static partial class CountryAssembler
    {
        static partial void OnDTO(this Country entity, CountryDto dto);
        static partial void OnEntity(this CountryDto dto, Country entity);

        public static Country ToEntity(this CountryDto dto)
        {
            if (dto == null) return null;

            var entity = new Country();

            entity.CountryId = dto.countryId;
            entity.Name = dto.name;
            entity.ISO31661Alpha2 = dto.iSO31661Alpha2;
            entity.ISO31661Alpha3 = dto.iSO31661Alpha3;
            entity.ISO31661Numeric = dto.iSO31661Numeric;
            entity.CurrencyCode = dto.currencyCode;
            entity.CurrencyCodeNumeric = dto.currencyCodeNumeric;
            entity.CurrencyFormat = dto.currencyFormat;
            entity.CultureName = dto.cultureName;
            entity.CultureLanguageName = dto.cultureLanguageName;
            entity.CultureCode = dto.cultureCode;

            dto.OnEntity(entity);

            return entity;
        }
        public static CountryDto ToDTO(this Country entity)
        {
            if (entity == null) return null;

            var dto = new CountryDto();

            dto.countryId = entity.CountryId;
            dto.name = entity.Name;
            dto.iSO31661Alpha2 = entity.ISO31661Alpha2;
            dto.iSO31661Alpha3 = entity.ISO31661Alpha3;
            dto.iSO31661Numeric = entity.ISO31661Numeric;
            dto.currencyCode = entity.CurrencyCode;
            dto.currencyCodeNumeric = entity.CurrencyCodeNumeric;
            dto.currencyFormat = entity.CurrencyFormat;
            dto.cultureName = entity.CultureName;
            dto.cultureLanguageName = entity.CultureLanguageName;
            dto.cultureCode = entity.CultureCode;

            entity.OnDTO(dto);

            return dto;
        }

        public static List<Country> ToEntities(this IEnumerable<CountryDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        public static List<CountryDto> ToDTOs(this IEnumerable<Country> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }
    }
}
