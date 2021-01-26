using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    internal class ParkingSpot : IMyParkingSpot
    {
        protected static int lotCount = 0;

        public ParkingSpot ()
        {
            ID = ++lotCount;
        }

        public int ID
        {
            get => default;
            set
            {
            }
        }

        public SpotType Type
        {
            get => default;
            set
            {
            }
        }

        public bool Occupied
        {
            get => default;
            set
            {
            }
        }

        public decimal SpotFee { get; set; }
    }
}