using Framework.Common.DTOs;
using System;
using System.Collections.Generic;

namespace Services.Security.Infrastructure.Entities
{
    public partial class UserDTO 
    {
        public UserDTO()
        {
            //UserRoles = new List<UserRolesDTO>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public IEnumerable<string> UserRoleIDs { get; set; }
        //public virtual List<UserRolesDTO> UserRoles { get; set; }
    }
}
