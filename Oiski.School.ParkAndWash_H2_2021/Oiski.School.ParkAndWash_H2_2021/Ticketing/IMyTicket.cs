using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines a ticket that is tied to an <see cref="Parking.IMyParkingSpot"/>
    /// </summary>
    public interface IMyTicket
    {
        int ID { get; }

        /// <summary>
        /// Set the <see langword="value"/> of a property
        /// </summary>
        /// <param name="_propertyName">The property name in <i>PascalCase</i></param>
        /// <param name="_value">The <see langword="value"/> to assign the property</param>
        void SetProperty (string _propertyName, object _value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns>An <see cref="Array"/> of <see cref="KeyValuePair{TKey, TValue}"/> <see langword="objects"/> where <strong>key</strong> is the property name in <i>PascalCase</i></returns>
        KeyValuePair<string, object>[] GetTicketProperties ();
    }
}