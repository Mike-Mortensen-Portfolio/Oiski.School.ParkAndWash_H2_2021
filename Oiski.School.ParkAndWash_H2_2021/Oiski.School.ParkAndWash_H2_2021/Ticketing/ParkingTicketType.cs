using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines a set of <see cref="IMyParkingTicket"/> types
    /// </summary>
    public enum ParkingTicketType
    {
        /// <summary>
        /// A basic ticket without any extended behavior
        /// </summary>
        Standard,
        /// <summary>
        /// An extended ticket that supports charging of vehicles
        /// </summary>
        ParkingCharge,
        /// <summary>
        /// An extended ticket that includes a car wash
        /// </summary>
        ParkingWash,
        /// <summary>
        /// And extended ticket that includes a service check for a vehicle
        /// </summary>
        ParkingService
    }
}
