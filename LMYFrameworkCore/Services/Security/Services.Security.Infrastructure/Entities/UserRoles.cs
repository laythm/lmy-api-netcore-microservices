using System;
using System.Collections.Generic;

namespace Services.Security.Infrastructure.Entities
{
    public partial class UserRoles
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual Users User { get; set; }
    }
}
