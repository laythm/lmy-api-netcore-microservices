using Framework.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Services.Security.Infrastructure.Entities
{
    public partial class UserRoles : ITrackable, IUserTrackable
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public virtual Roles Role { get; set; }
        public virtual Users User { get; set; }
    }
}
