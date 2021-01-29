using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines a Ticket
    /// </summary>
    internal class ParkingTicket : IMyParkingTicket, IMyRepositoryEntity<int, string>
    {
        protected static int ticketCount = 0;

        /// <summary>
        /// Creates a new instance of type <see cref="ParkingTicket"/>
        /// </summary>
        internal ParkingTicket ()
        {

        }

        /// <summary>
        /// Creates a new instance of type <see cref="ParkingTicket"/> where the parking spot ID and hourly parking cost is set
        /// </summary>
        /// <param name="_parkingSpotID"></param>
        /// <param name="_pricePrHour"></param>
        public ParkingTicket (int _parkingSpotID, decimal _pricePrHour)
        {
            ID = ++ticketCount;
            ParkingSpotID = _parkingSpotID;
            OccupationStamp = DateTime.Now;
            OccupationPricePrHour = _pricePrHour;
        }

        /// <summary>
        /// The ID of the <see cref="Parking.IMyParkingSpot"/> attached to this <see cref="IMyTicket"/>
        /// </summary>
        public int ParkingSpotID { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// The exact timestamp for when this <see cref="IMyTicket"/> was generated
        /// </summary>
        public DateTime OccupationStamp { get; set; }
        /// <summary>
        /// The cost for occupating an <see cref="Parking.IMyParkingSpot"/> fo a hour
        /// </summary>
        public decimal OccupationPricePrHour { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A <see langword="string"/> containing each property value for <see langword="this"/> instance, seperated by comma</returns>
        public virtual string SaveEntity ()
        {
            return $"{ID},{OccupationStamp},{OccupationPricePrHour},{ParkingSpotID}";
        }

        /// <summary>
        /// Rebuild <see langword="this"/> instance based on the passed in <see langword="string"/> <paramref name="_values"/>
        /// </summary>
        /// <param name="_values"></param>
        /// <exception cref="InvalidDataException"></exception>
        public virtual void BuildEntity (string _values)
        {
            string[] values = _values.Split(",");

            if ( int.TryParse(values[0], out int _id) && DateTime.TryParse(values[1], out DateTime _stamp) && decimal.TryParse(values[2], out decimal _spotFee) && int.TryParse(values[3], out int _spotID) )
            {
                this.ID = _id;
                this.OccupationStamp = _stamp;
                this.OccupationPricePrHour = _spotFee;
                this.ParkingSpotID = _spotID;
            }
            else
            {
                throw new InvalidDataException($"One or more fields couldn't be retrieved from: {_values}");
            }
        }

        public virtual KeyValuePair<string, object>[] GetTicketProperties ()
        {
            KeyValuePair<string, object>[] properties =
            {
                KeyValuePair.Create ( "ParkingSpotID",(object)ParkingSpotID),
                KeyValuePair.Create ( "OccupationStamp",(object)OccupationStamp),
                KeyValuePair.Create ( "OccupationPricePrHour",(object)OccupationPricePrHour)
            };

            return properties;
        }

        public virtual void SetProperty (string _propertyName, object _value)
        {
            try
            {
                switch ( _propertyName )
                {
                    case "ParkingSpotID":
                        ParkingSpotID = ( int ) _value;
                        break;
                    case "OccupationStamp":
                        OccupationStamp = ( DateTime ) _value;
                        break;
                    case "OccupationPricePrHour":
                        OccupationPricePrHour = ( decimal ) _value;
                        break;
                    default:
                        throw new PropertyNotFoundException<IMyParkingTicket>(GetTicketProperties());
                }
            }
            catch ( InvalidCastException _e )
            {
                throw new InvalidCastException("Invalid Property Value", _e);
            }

        }
    }
}