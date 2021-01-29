using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines a Ticket
    /// </summary>
    internal class ParkingTicket : IMyParkingTicket
    {
        protected static int ticketCount = 0;

        /// <summary>
        /// Creates a new instance of type <see cref="ParkingTicket"/>
        /// </summary>
        internal ParkingTicket ()
        {

        }

        /// <summary>
        /// Creates a new instance of type <see cref="ParkingTicket"/> where the parking spot ID and hourly parking cost is set
        /// </summary>
        /// <param name="_parkingSpotID"></param>
        /// <param name="_pricePrHour"></param>
        public ParkingTicket (int _parkingSpotID, decimal _pricePrHour)
        {
            ID = ++ticketCount;
            ParkingSpotID = _parkingSpotID;
            OccupationStamp = DateTime.Now;
            OccupationPricePrHour = _pricePrHour;
        }

        /// <summary>
        /// The ID of the <see cref="Parking.IMyParkingSpot"/> attached to this <see cref="IMyTicket"/>
        /// </summary>
        public int ParkingSpotID { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// The exact timestamp for when this <see cref="IMyTicket"/> was generated
        /// </summary>
        public DateTime OccupationStamp { get; set; }
        /// <summary>
        /// The cost for occupating an <see cref="Parking.IMyParkingSpot"/> fo a hour
        /// </summary>
        public decimal OccupationPricePrHour { get; set; }
    }
}