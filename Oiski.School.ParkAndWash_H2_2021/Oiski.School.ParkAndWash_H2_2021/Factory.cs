using System;
using Oiski.School.ParkAndWash_H2_2021.Parking;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;

namespace Oiski.School.ParkAndWash_H2_2021
{
    public static class Factory
    {
        public static IMyService<IMyParkingSpot> CreateParkingService ()
        {
            //  Create new instance of ParkingService class
            throw new NotImplementedException();
        }

        public static IMyService<IMyTicket> CreateTicketService ()
        {
            //  Create new instance of TicketService class
            throw new NotImplementedException();
        }

        public static IMyParkingSpot CreateParkingSpot (SpotType _type)
        {
            //  Create new instance of ParkingSpot (_type) class
            throw new NotImplementedException();
        }

        public static IMyTicket CreateTicket (int _parkingSpotID, decimal _pricePrHour)
        {
            //  Create new instance of Ticket (_parkingSpotID, _pricePrHour) class
            throw new NotImplementedException();
        }
    }
}
