using MoM.Module.Models;
using System.Collections.Generic;
using System.Linq;

namespace MoM.Module.Dtos
{
    public static partial class LanguageAssembler
    {
        static partial void OnDTO(this Language entity, LanguageDto dto);
        static partial void OnEntity(this LanguageDto dto, Language entity);
        public static Language ToEntity(this LanguageDto dto)
        {
            if (dto == null) return null;

            var entity = new Language();

            entity.LanguageId = dto.key;
            entity.Country = dto.country.ToEntity();
            dto.OnEntity(entity);

            return entity;
        }
        public static LanguageDto ToDTO(this Language entity)
        {
            if (entity == null) return null;

            var dto = new LanguageDto();

            dto.key = entity.LanguageId;
            dto.country = entity.Country.ToDTO();

            entity.OnDTO(dto);

            return dto;
        }

        public static List<Language> ToEntities(this IEnumerable<LanguageDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        public static List<LanguageDto> ToDTOs(this IEnumerable<Language> entities)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO()).ToList();
        }
    }
}
