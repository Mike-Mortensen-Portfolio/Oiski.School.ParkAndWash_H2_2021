﻿using System;
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
        public static IMyParkingSpot CreateParkingSpot (SpotType _type)
        {
            return new ParkingSpot(_type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_parkingSpotID"></param>
        /// <param name="_pricePrHour"></param>
        /// <returns>A new instance of <see cref="IMyTicket"/> with a linked <see cref="IMyParkingSpot"/>.</returns>
        public static IMyTicket CreateTicket (int _parkingSpotID, decimal _pricePrHour)
        {
            return new Ticket(_parkingSpotID, _pricePrHour);
        }
    }
}
