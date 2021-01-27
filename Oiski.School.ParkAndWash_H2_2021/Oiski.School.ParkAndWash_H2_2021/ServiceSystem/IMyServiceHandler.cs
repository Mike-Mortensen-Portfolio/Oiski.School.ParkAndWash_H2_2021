using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    public interface IMyServiceHandler
    {
        IReadOnlyList<IMyServiceBase> Services { get; }

        IMyServiceBase this[string _serviceID] { get; }

        void InjectService (IMyServiceBase _service);
        bool RemoveService (IMyServiceBase _service);
        T GetServiceAs<T> (string _serviceID);
    }
}