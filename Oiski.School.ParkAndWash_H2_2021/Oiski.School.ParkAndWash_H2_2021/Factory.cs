using System;
using Oiski.School.ParkAndWash_H2_2021.Parking;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;
using Oiski.School.ParkAndWash_H2_2021.Washing;

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
            return new ParkingService ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A new instance of <see cref="IMyService{T}"/> where T is an <see cref="IMyTicket"/></returns>
        public static IMyService<IMyTicket> CreateTicketService ()
        {
            return new TicketService ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A new instane of <see cref="IMyService{T}"/> where T is an <see cref="IMyCarWash"/></returns>
        public static IMyService<IMyCarWash> CreateCarWashService ()
        {
            return new CarWashService ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type"></param>
        /// <returns>A new instance of <see cref="IMyParkingSpot"/> where the type is <paramref name="_type"/></returns>
        /// <exception cref="OverflowException"></exception>
        public static IMyParkingSpot CreateParkingSpot ( SpotType _type )
        {
            return new ParkingSpot (_type);
        }
        internal static IMyParkingSpot CreateDefaultParkingSpot ()
        {
            return new ParkingSpot ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_parkingSpotID"></param>
        /// <param name="_pricePrHour"></param>
        /// <returns>A new instance of <see cref="IMyTicket"/> with a linked <see cref="IMyParkingSpot"/>.</returns>
        /// <exception cref="OverflowException"></exception>
        public static IMyTicket CreateParkingTicket ( int _parkingSpotID, decimal _pricePrHour )
        {
            return new ParkingTicket (_parkingSpotID, _pricePrHour);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type"></param>
        /// <returns>A new instance of <see cref="IMyTicket"/> that matches the passed in <see cref="ParkingTicketType"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static IMyTicket CreateParkingTicket ( ParkingTicketType _type )
        {
            return _type switch
            {
                ParkingTicketType.Standard => new ParkingTicket (0, 0),
                ParkingTicketType.ParkingCharge => new ParkingChargeTicket (0, 0, 0),
                ParkingTicketType.ParkingWash => new ParkingWashTicket (0, 0, string.Empty),
                ParkingTicketType.ParkingService => new ParkingServiceTicket (0, 0, string.Empty),
                _ => throw new ArgumentException ($"Type: {_type} is not valid in this context!"),
            };
        }
        internal static IMyTicket CreateDefaultParkingTicket ()
        {
            return new ParkingTicket ();
        }

        public static IMyTicket CreateCarWashTicket ( int _washID, CarWashType _type )
        {
            throw new NotImplementedException ();
        }
        public static IMyTicket CreateCarWashTicket ( CarWashType _type )
        {
            throw new NotImplementedException ();
        }
        internal static IMyTicket CreateDefaultCarWashTicket ()
        {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <returns>A new instance of a basic <see cref="IMyCarWash"/> where the name is set</returns>
        public static IMyCarWash CreateCarWash ( string _name )
        {
            return new BronzeWash (_name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name">The name of the car wash</param>
        /// <param name="_type">The type of wash</param>
        /// <returns>A new instance of <see cref="IMyCarWash"/> that matches the passed in <see cref="CarWashType"/></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IMyCarWash CreateCarWash ( string _name, CarWashType _type )
        {
            return _type switch
            {
                CarWashType.Bronze => new BronzeWash (_name),
                CarWashType.Silver => new SilverWash (_name),
                CarWashType.Gold => new GoldWash (_name),
                _ => throw new ArgumentException ($"Type: {_type} is not valid in this context!")
            };
        }
        internal static IMyCarWash CreateDefaultCarWash ()
        {
            return new BronzeWash ();
        }
    }
}
