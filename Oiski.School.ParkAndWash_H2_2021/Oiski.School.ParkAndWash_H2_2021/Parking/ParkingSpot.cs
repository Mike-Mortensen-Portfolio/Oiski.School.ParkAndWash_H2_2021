using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    internal class ParkingSpot : IMyParkingSpot
    {
        protected static int lotCount = 0;

        public ParkingSpot (SpotType _type)
        {
            ID = ++lotCount;
            Type = _type;
        }

        public int ID { get; set; }

        public SpotType Type { get; set; }

        public bool Occupied { get; set; }

        public decimal SpotFee { get; set; }
    }
}