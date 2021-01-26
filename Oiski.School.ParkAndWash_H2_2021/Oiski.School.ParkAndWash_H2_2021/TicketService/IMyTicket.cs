using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.TicketService
{
    public interface IMyTicket
    {
        int ParkingSpotID { get; }
        int ID { get; }
        DateTime OccupationStamp { get; }
        decimal OccupationPricePrHour { get; }
    }
}