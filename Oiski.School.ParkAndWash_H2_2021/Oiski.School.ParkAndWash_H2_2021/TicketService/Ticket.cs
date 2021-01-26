using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.TicketService
{
    internal class Ticket : IMyTicket
    {
        protected static int ticketCount = 0;

        public Ticket (int _parkingSpotID, decimal _pricePrHour)
        {
            ID = ++ticketCount;
            ParkingSpotID = _parkingSpotID;
            OccupationStamp = DateTime.Now;
            OccupationPricePrHour = _pricePrHour;
        }

        public int ParkingSpotID { get; }
        public int ID { get; }
        public DateTime OccupationStamp { get; set; }
        public decimal OccupationPricePrHour { get; set; }
    }
}