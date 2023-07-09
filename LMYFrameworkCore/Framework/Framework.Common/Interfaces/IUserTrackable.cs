using System;

namespace Framework.Common.Interfaces
{
    public interface IUserTrackable
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}
