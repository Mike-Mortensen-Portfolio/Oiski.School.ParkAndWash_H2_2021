using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    public interface IMyServiceItemCollection<T>
    {
        IReadOnlyList<T> Items { get; }

        void AddServiceItem (T _item);
        bool RemoveServiceItem (T _item);
        T FindServiceItem (Predicate<T> _predicate);
        IReadOnlyList<T> FindAllServiceItems (Predicate<T> Predicate);
    }
}