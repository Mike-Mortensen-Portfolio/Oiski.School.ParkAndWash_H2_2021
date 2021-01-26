using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    internal class ParkingLot : IMyParkingHandler
    {
        protected List<IMyParkingSpot> parkingSpots;

        public ParkingLot ()
        {
            parkingSpots = new List<IMyParkingSpot>();
        }

        public IReadOnlyList<IMyParkingSpot> ParkingSpots
        {
            get => default;
            set
            {
            }
        }

        public IReadOnlyList<IMyParkingSpot> GetAvailableParkingSpots (SpotType _type)
        {
            throw new System.NotImplementedException();
        }

        public bool OccupyParkingSpot (SpotType _type, string _customerEmail)
        {
            throw new System.NotImplementedException();
        }

        public bool FreeParkingSpot (int _spotID)
        {
            throw new System.NotImplementedException();
        }
    }
}