using System;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    public interface IMyParkingSpot
    {
        int ID { get; }
        bool Occupied { get; set; }
        SpotType Type { get; }
        decimal SpotFee { get; set; }
    }
}