using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    public interface IMyParkingTicket : IMyTicket
    {
        /// <summary>
        /// The ID of the <see cref="Parking.IMyParkingSpot"/> attached to this <see cref="IMyTicket"/>
        /// </summary>
        int ParkingSpotID { get; }
        /// <summary>
        /// The exact timestamp for when this <see cref="IMyTicket"/> was generated
        /// </summary>
        DateTime OccupationStamp { get; }

        /// <summary>
        /// The cost for occupatig an <see cref="Parking.IMyParkingSpot"/> fo a hour
        /// </summary>
        decimal OccupationPricePrHour { get; }
    }
}
