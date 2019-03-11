using Framework.Common.DTOs;
using System;
using System.Collections.Generic;

namespace Services.Security.Infrastructure.Entities
{
    public partial class UserRolesDTO : DTOBase
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual RoleDTO Role { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
