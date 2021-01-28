using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    /// <summary>
    /// Defines a set of types a <see cref="IMyParkingSpot"/> can have
    /// </summary>
    public enum SpotType
    {
        /// <summary>
        /// Can hold normal class vehicles
        /// </summary>
        Standard,
        /// <summary>
        /// Special parking spots that can hold a various types of utility units.
        /// </summary>
        Util,
        /// <summary>
        /// Can hold large vehicles like a bus
        /// </summary>
        Large,
        /// <summary>
        /// Spot reserved for handicap parking
        /// </summary>
        Handicap
    }
}