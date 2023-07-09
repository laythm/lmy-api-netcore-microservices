using System;

namespace Framework.Common.Interfaces
{
    public interface ITrackable
    {
        DateTime CreationDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}
