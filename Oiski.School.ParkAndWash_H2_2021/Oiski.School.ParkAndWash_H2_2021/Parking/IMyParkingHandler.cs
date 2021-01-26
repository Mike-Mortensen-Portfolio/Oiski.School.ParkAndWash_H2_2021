using System.Collections.Generic;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    public interface IMyParkingHandler
    {
        IReadOnlyList<IMyParkingSpot> ParkingSpots { get; }

        bool FreeParkingSpot (int _spotID);
        IReadOnlyList<IMyParkingSpot> GetAvailableParkingSpots (SpotType _type);
        bool OccupyParkingSpot (SpotType _type, string _customerEmail);
    }
}