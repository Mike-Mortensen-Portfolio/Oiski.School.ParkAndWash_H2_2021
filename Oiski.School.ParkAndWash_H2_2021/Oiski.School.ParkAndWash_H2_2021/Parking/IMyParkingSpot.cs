using System;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    /// <summary>
    /// Defines a parkingspot
    /// </summary>
    public interface IMyParkingSpot
    {
        int ID { get; }
        bool Occupied { get; set; }
        SpotType Type { get; }
        /// <summary>
        /// The fee that is added on top of the hourly parking price
        /// </summary>
        decimal SpotFee { get; set; }
    }
}