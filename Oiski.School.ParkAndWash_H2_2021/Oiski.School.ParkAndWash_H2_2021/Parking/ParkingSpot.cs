using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Globalization;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    /// <summary>
    /// Defines a Parking Spot
    /// </summary>
    internal class ParkingSpot : IMyParkingSpot, IMyRepositoryEntity<int, string>
    {
        protected static int lotCount = 0;

        /// <summary>
        /// Creates a new instance of type <see cref="ParkingSpot"/>
        /// </summary>
        internal ParkingSpot ()
        {
            ID = ++lotCount;
        }

        /// <summary>
        /// Creates a new instance of type <see cref="ParkingSpot"/> where the <see cref="SpotType"/> is set
        /// </summary>
        /// <param name="_type"></param>
        /// <exception cref="OverflowException"></exception>
        public ParkingSpot ( SpotType _type )
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A <see langword="string"/> containing each property value for <see langword="this"/> instance, seperated by comma</returns>
        public string SaveEntity ()
        {
            return $"ID{ID},{( int ) Type},{Occupied},{SpotFee.ToString(CultureInfo.CreateSpecificCulture ("en-GB"))}";
        }

        /// <summary>
        /// Rebuild <see langword="this"/> instance based on the passed in <see langword="string"/> <paramref name="_values"/>
        /// </summary>
        /// <param name="_values"></param>
        /// <exception cref="InvalidDataException"></exception>
        public void BuildEntity ( string _values )
        {
            string[] values = _values.Split (",");

            if ( int.TryParse (values[ 0 ].Replace ("ID", string.Empty), out int _id) && int.TryParse (values[ 1 ], out int _type) && bool.TryParse (values[ 2 ], out bool _occupied) && decimal.TryParse (values[ 3 ], out decimal _spotFee) )
            {
                this.ID = _id;
                this.Occupied = _occupied;
                this.SpotFee = _spotFee;
                this.Type = ( SpotType ) _type;
            }
            else
            {
                throw new InvalidDataException ($"One or more fields couldn't be retrieved from: {_values}");
            }
        }
    }
}