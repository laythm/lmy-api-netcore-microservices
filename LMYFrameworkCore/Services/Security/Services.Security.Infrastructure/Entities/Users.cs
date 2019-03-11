using System;
using System.Collections.Generic;

namespace Services.Security.Infrastructure.Entities
{
    public partial class Users
    {
        public Users()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
