using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    public interface IMyService<T>
    {
        IReadOnlyList<T> ServiceContainer { get; }
        T GenerateServiceItem ();
        bool ValidateServiceItem<IDType> (IDType _serviceItemID);
        IReadOnlyList<T> GetServiceItemsBy<IDType> (IDType _identifier);
        void HandleServiceItem<IDType> (IDType _identifier);
    }
}