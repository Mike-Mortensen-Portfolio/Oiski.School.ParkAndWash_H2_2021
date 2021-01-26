using Oiski.School.ParkAndWash_H2_2021.Parking;

namespace Oiski.School.ParkAndWash_H2_2021
{
    public static class Factory
    {
        public static IMyParkingHandler CreateParkingHandler ()
        {
            return new ParkingLot();
        }

        public static IMyService<IMyTicket> CreateTicketService ()
        {
            return new TicketService();
        }

        public static IMyParkingSpot CreateParkingSpot ()
        {
            return new ParkingSpot();
        }

        public static IMyTicket CreateTicket (int _parkingSpotID, decimal _pricePrHour)
        {
            return new Ticket(_parkingSpotID, _pricePrHour);
        }
    }
}
