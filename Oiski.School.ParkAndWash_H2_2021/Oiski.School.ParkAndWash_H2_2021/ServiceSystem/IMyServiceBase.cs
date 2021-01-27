using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    public interface IMyServiceBase
    {
        string ServiceID { get; }

        bool ChangeServiceID (string _newID);
    }
}