using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// Defines the <see langword="base"/> type for service <see langword="objects"/>
    /// </summary>
    public interface IMyServiceBase
    {
        string ServiceID { get; }

        bool ChangeServiceID (string _newID);
    }
}