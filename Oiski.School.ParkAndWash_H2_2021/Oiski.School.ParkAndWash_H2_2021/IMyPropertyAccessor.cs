using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// Defines an <see langword="interface"/> that allows properies to be accessed without having access to the <strong>real</strong> type that contains the properties
    /// </summary>
    public interface IMyPropertyAccessor
    {
        /// <summary>
        /// Set the <see langword="value"/> of a property
        /// </summary>
        /// <param name="_propertyName">The property name in <i>PascalCase</i></param>
        /// <param name="_value">The <see langword="value"/> to assign the property</param>
        void SetProperty ( string _propertyName, object _value );

        /// <summary>
        /// Gets a collection of <see cref="KeyValuePair{TKey, TValue}"/> that represents the properties that are available to the <see cref="IMyPropertyAccessor"/> <see langword="interface"/>
        /// </summary>
        /// <returns>An <see cref="Array"/> of <see cref="KeyValuePair{TKey, TValue}"/> <see langword="objects"/> where <strong>key</strong> is the property name in <i>PascalCase</i></returns>
        KeyValuePair<string, object>[] GetTicketProperties ();
    }
}
