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

        void SetProperty (string _propertyName, object _value);

        KeyValuePair<string, object>[] GetTicketProperties ();
    }
}