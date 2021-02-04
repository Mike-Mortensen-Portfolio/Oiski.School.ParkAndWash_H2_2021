using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// A collection of <typeparamref name="T"/> items that can be addded ad/or removed from 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMyServiceItemCollection<T>
    {
        IReadOnlyList<T> Items { get; }

        void AddServiceItem (T _item);
        bool RemoveServiceItem (T _item);
        T FindServiceItem (Predicate<T> _predicate);
        IReadOnlyList<T> FindAllServiceItems (Predicate<T> _predicate);
    }
}