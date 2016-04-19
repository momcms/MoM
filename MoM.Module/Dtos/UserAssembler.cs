using MoM.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoM.Module.Dtos
{
    public static partial class UserAssembler
    {
        /// <summary>
        /// Invoked when <see cref="ToDTO"/> operation is about to return.
        /// </summary>
        /// <param name="dto"><see cref="UserDto"/> converted from <see cref="ApplicationUser"/>.</param>
        static partial void OnDTO(this ApplicationUser entity, UserDto dto);

        /// <summary>
        /// Invoked when <see cref="ToEntity"/> operation is about to return.
        /// </summary>
        /// <param name="entity"><see cref="ApplicationUser"/> converted from <see cref="UserDto"/>.</param>
        static partial void OnEntity(this UserDto dto, ApplicationUser entity);

        /// <summary>
        /// Converts this instance of <see cref="UserDto"/> to an instance of <see cref="ApplicationUser"/>.
        /// </summary>
        /// <param name="dto"><see cref="UserDto"/> to convert.</param>
        public static ApplicationUser ToEntity(this UserDto dto)
        {
            if (dto == null) return null;

            var entity = new ApplicationUser();

            entity.Id = dto.id;
            entity.UserName = dto.userName;
            entity.Email = dto.email;
            entity.EmailConfirmed = dto.emailConfirmed;
            entity.AccessFailedCount = dto.accessFailedCount;
            entity.ConcurrencyStamp = dto.concurrencyStamp;
            entity.LockoutEnabled = dto.lockoutEnabled;
            entity.LockoutEnd = dto.lockoutEnd;
            entity.PhoneNumber = dto.phoneNumber;
            entity.PhoneNumberConfirmed = dto.phoneNumberConfirmed;
            entity.TwoFactorEnabled = dto.twoFactorEnabled;

            dto.OnEntity(entity);

            return entity;
        }

        /// <summary>
        /// Converts this instance of <see cref="ApplicationUser"/> to an instance of <see cref="UserDto"/>.
        /// </summary>
        /// <param name="entity"><see cref="ApplicationUser"/> to convert.</param>
        public static UserDto ToDTO(this ApplicationUser entity, bool includePost = false)
        {
            if (entity == null) return null;

            var dto = new UserDto();

            dto.id = entity.Id;
            dto.userName = entity.UserName;
            dto.email = entity.Email;
            dto.emailConfirmed = entity.EmailConfirmed;
            dto.accessFailedCount = entity.AccessFailedCount;
            dto.concurrencyStamp = entity.ConcurrencyStamp;
            dto.lockoutEnabled = entity.LockoutEnabled;
            dto.lockoutEnd = entity.LockoutEnd;
            dto.phoneNumber = entity.PhoneNumber;
            dto.phoneNumberConfirmed = entity.PhoneNumberConfirmed;
            dto.twoFactorEnabled = entity.TwoFactorEnabled;
            //dto.roles = entity.Roles.ToDTOs();

            entity.OnDTO(dto);

            return dto;
        }

        /// <summary>
        /// Converts each instance of <see cref="UserDto"/> to an instance of <see cref="ApplicationUser"/>.
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static List<ApplicationUser> ToEntities(this IEnumerable<UserDto> dtos)
        {
            if (dtos == null) return null;

            return dtos.Select(e => e.ToEntity()).ToList();
        }

        /// <summary>
        /// Converts each instance of <see cref="ApplicationUser"/> to an instance of <see cref="UserDto"/>.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<UserDto> ToDTOs(this IEnumerable<ApplicationUser> entities, bool includePosts = false)
        {
            if (entities == null) return null;

            return entities.Select(e => e.ToDTO(includePosts)).ToList();
        }
    }
}
