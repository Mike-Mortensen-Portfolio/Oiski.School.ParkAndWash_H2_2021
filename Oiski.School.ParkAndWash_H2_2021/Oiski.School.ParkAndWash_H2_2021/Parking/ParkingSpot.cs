using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    /// <summary>
    /// Defines a Parking Spot
    /// </summary>
    internal class ParkingSpot : IMyParkingSpot
    {
        protected static int lotCount = 0;

        /// <summary>
        /// Creates a new instance of type <see cref="ParkingSpot"/>
        /// </summary>
        internal ParkingSpot ()
        {

        }

        /// <summary>
        /// Creates a new instance of type <see cref="ParkingSpot"/> where the <see cref="SpotType"/> is set
        /// </summary>
        /// <param name="_type"></param>
        public ParkingSpot (SpotType _type)
        {
            ID = ++lotCount;
            Type = _type;
        }

        public int ID { get; set; }

        public SpotType Type { get; set; }

        public bool Occupied { get; set; }

        /// <summary>
        /// The fee that is added on top of the hourly parking price
        /// </summary>
        public decimal SpotFee { get; set; }
    }
}