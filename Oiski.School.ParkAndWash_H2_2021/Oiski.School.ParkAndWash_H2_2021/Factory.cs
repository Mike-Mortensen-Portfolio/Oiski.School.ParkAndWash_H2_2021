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
        /// Create a new instance of an <see cref="IMyTicket"/> based on the type <see langword="string"/> <paramref name="_ticketType"/>
        /// </summary>
        /// <param name="_ticketType">The full type name of the ticket. (<i><strong>Note: </strong> Type.FullName</i>)</param>
        /// <returns>A new instance of type <paramref name="_ticketType"/> if type exists; Otherwise, <see langword="null"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static IMyTicket CreateDefaultTicket ( string _ticketType )
        {
            if ( _ticketType == typeof (ParkingTicket).FullName )
            {
                return CreateDefaultParkingTicket ();
            }
            else if ( _ticketType == typeof (ParkingChargeTicket).FullName )
            {
                return CreateParkingTicket (ParkingTicketType.ParkingCharge);
            }
            else if ( _ticketType == typeof (ParkingServiceTicket).FullName )
            {
                return CreateParkingTicket (ParkingTicketType.ParkingService);
            }
            else if ( _ticketType == typeof (ParkingWashTicket).FullName )
            {
                return CreateParkingTicket (ParkingTicketType.ParkingWash);
            }
            else if ( _ticketType == typeof (CarWashTicket).FullName )
            {
                return CreateDefaultCarWashTicket ();
            }

            return null;
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
                ParkingTicketType.ParkingWash => new ParkingWashTicket (0, 0, CarWashType.Bronze),
                ParkingTicketType.ParkingService => new ParkingServiceTicket (0, 0, string.Empty),
                _ => throw new ArgumentException ($"Type: {_type} is not valid in this context!"),
            };
        }
        internal static IMyTicket CreateDefaultParkingTicket ()
        {
            return new ParkingTicket ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_washID"></param>
        /// <param name="_washPrice"></param>
        /// <param name="_type"></param>
        /// <returns>A new instance of <see cref="IMyTicket"/> with a linked <see cref="IMyCarWash"/>.</returns>
        /// <exception cref="OverflowException"></exception>
        public static IMyTicket CreateCarWashTicket ( int _washID, decimal _washPrice, CarWashType _type )
        {
            return new CarWashTicket (_washID, _washPrice, _type);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type"></param>
        /// <returns>A new instance of <see cref="IMyTicket"/> that matches the passed in <see cref="CarWashType"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static IMyTicket CreateCarWashTicket ( CarWashType _type )
        {
            return new CarWashTicket (0, 0, _type);
        }
        internal static IMyTicket CreateDefaultCarWashTicket ()
        {
            return new CarWashTicket ();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name">The name of the car wash</param>
        /// <returns>A new instance of a basic <see cref="IMyCarWash"/> where the name is set</returns>
        /// <exception cref="OverflowException"></exception>
        public static IMyCarWash CreateCarWash ( string _name )
        {
            return new CarWash (_name, new CarWashState[] { CarWashState.Soaping, CarWashState.Scrubbing, CarWashState.Blasting, CarWashState.Drying });
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
                CarWashType.Bronze => new CarWash (_name, new CarWashState[] { CarWashState.Soaping, CarWashState.Scrubbing, CarWashState.Blasting, CarWashState.Drying }),
                CarWashType.Silver => new CarWash (_name, new CarWashState[] { CarWashState.Soaping, CarWashState.Scrubbing, CarWashState.Rinsing, CarWashState.Blasting, CarWashState.Drying }),
                CarWashType.Gold => new CarWash (_name, new CarWashState[] { CarWashState.Soaping, CarWashState.Scrubbing, CarWashState.Rinsing, CarWashState.Waxing, CarWashState.Blasting, CarWashState.Drying }),
                _ => throw new ArgumentException ($"Type: {_type} is not valid in this context!")
            };
        }
        internal static IMyCarWash CreateDefaultCarWash ()
        {
            return new CarWash ();
        }
    }
}
