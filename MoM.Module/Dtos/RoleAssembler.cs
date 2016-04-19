using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace MoM.Module.Dtos
{
    public static partial class RoleAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="RoleDto"/> converted from <see cref="IdentityRole"/>.</param>
        static partial void OnDTO(this IdentityRole entity, RoleDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="IdentityRole"/> converted from <see cref="RoleDto"/>.</param>
        static partial void OnEntity(this RoleDto dto, IdentityRole entity);

        /// <summary>
        /// Converts this instance of <see cref="RoleDto"/> to an instance of <see cref="IdentityRole"/>.
        /// </summary>
        /// <param name="dto"><see cref="RoleDto"/> to convert.</param>
        public static IdentityRole ToEntity(this RoleDto dto)
        {
            if (dto == null) return null;

            var entity = new IdentityRole();

            entity.Id = dto.id;
            entity.Name = dto.name;
            entity.ConcurrencyStamp = dto.concurrencyStamp;
            entity.NormalizedName = dto.normalizedName;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="IdentityRole"/> to an instance of <see cref="RoleDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="IdentityRole"/> to convert.</param>
        public static RoleDto ToDTO(this IdentityRole entity, bool includeUsers = false)
        {
            if (entity == null) return null;

            var dto = new RoleDto();

            dto.id = entity.Id;
            dto.name = entity.Name;
            dto.concurrencyStamp = entity.ConcurrencyStamp;
            dto.normalizedName = entity.NormalizedName;
            if (includeUsers)
            {
                //dto.postCount = entity.Posts.Where(p => p.IsPublished == 1).Count();
            }

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="RoleDto"/> to an instance of <see cref="IdentityRole"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<IdentityRole> ToEntities(this IEnumerable<RoleDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="IdentityRole"/> to an instance of <see cref="RoleDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<RoleDto> ToDTOs(this IEnumerable<IdentityRole> entities, bool includeUsers = false)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO(includeUsers)).ToList();
        }
    }
}
