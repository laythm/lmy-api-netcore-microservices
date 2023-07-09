using Framework.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Services.Security.Infrastructure.Entities
{
    public partial class Roles: ITrackable, IUserTrackable
    {
        public Roles()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
