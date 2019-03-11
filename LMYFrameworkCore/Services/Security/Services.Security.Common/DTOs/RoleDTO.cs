using Framework.Common.DTOs;
using System;
using System.Collections.Generic;

namespace Services.Security.Infrastructure.Entities
{
    public partial class RoleDTO: DTOBase
    {
        public RoleDTO()
        {
            UserRoles = new List<UserRolesDTO>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual List<UserRolesDTO> UserRoles { get; set; }
    }
}
