using System;
using Oiski.School.ParkAndWash_H2_2021.Parking;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// An <see langword="object"/> that contains factorial functionality related to the instantiations of other <see langword="objects"/>
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>A new instance of <see cref="IMyService{T}"/> where T is an <see cref="IMyParkingSpot"/></returns>
        public static IMyService<IMyParkingSpot> CreateParkingService ()
        {
            return new ParkingService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A new instance of <see cref="IMyService{T}"/> where T is an <see cref="IMyTicket"/></returns>
        public static IMyService<IMyTicket> CreateTicketService ()
        {
            return new TicketService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type"></param>
        /// <returns>A new instance of <see cref="IMyParkingSpot"/> where the type is <paramref name="_type"/></returns>
        /// <exception cref="OverflowException"></exception>
        public static IMyParkingSpot CreateParkingSpot (SpotType _type)
        {
            return new ParkingSpot(_type);
        }
        internal static IMyParkingSpot CreateDefaultParkingSpot ()
        {
            return new ParkingSpot();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_parkingSpotID"></param>
        /// <param name="_pricePrHour"></param>
        /// <returns>A new instance of <see cref="IMyTicket"/> with a linked <see cref="IMyParkingSpot"/>.</returns>
        /// <exception cref="OverflowException"></exception>
        public static IMyTicket CreateParkingTicket (int _parkingSpotID, decimal _pricePrHour)
        {
            return new ParkingTicket(_parkingSpotID, _pricePrHour);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type"></param>
        /// <returns>A new instance of <see cref="IMyTicket"/> that matches the passed in <see cref="ParkingTicketType"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static IMyTicket CreateParkingTicket (ParkingTicketType _type)
        {
            return _type switch
            {
                ParkingTicketType.Standard => new ParkingTicket(0, 0),
                ParkingTicketType.ParkingCharge => new ParkingChargeTicket(0, 0, 0),
                ParkingTicketType.ParkingWash => new ParkingWashTicket(0, 0, string.Empty),
                ParkingTicketType.ParkingService => new ParkingServiceTicket(0, 0, string.Empty),
                _ => throw new ArgumentException($"Type: {_type} is not valid in this context!"),
            };
        }
        internal static IMyTicket CreateDefaultParkingTicket ()
        {
            return new ParkingTicket();
        }
    }
}
