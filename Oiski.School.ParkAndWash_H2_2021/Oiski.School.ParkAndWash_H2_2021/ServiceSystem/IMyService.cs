using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// Defines a service <see langword="object"/>  that can hold a collection of <typeparamref name="T"/> items
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMyService<T> : IMyServiceBase, IMyServiceItemCollection<T>
    {
        /// <summary>
        /// Request an item from the <see cref="IMyService{T}"/>
        /// </summary>
        /// <typeparam name="ValueType"></typeparam>
        /// <param name="_value"></param>
        /// <returns>An instance of type <typeparamref name="T"/> if the request could be accepted; Otherwise the <see langword="default"/> value for <typeparamref name="T"/></returns>
        T RequestServiceItem<ValueType> (ValueType _value);
        bool CancelServiceItem<IDType> (IDType _itemID);
        bool ValidateServiceItem<IDType> (IDType _itemID);
    }
}