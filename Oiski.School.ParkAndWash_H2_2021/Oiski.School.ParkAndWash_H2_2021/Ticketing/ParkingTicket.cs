using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines a basic <see cref="IMyTicket"/> for an <see cref="Parking.IMyParkingSpot"/>
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
        /// Creates a new instance of type <see cref="ParkingTicket"/> where the <paramref name="_parkingSpotID"/> and <paramref name="_pricePrHour"/> is set
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
        /// Save the current state of the <see cref="ParkingTicket"/>
        /// </summary>
        /// <returns>The current state of <see langword="this"/> <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> as an instance of type <typeparamref name="SaveType"/></returns>
        public virtual string SaveEntity ()
        {
            return $"{ID},{OccupationStamp},{OccupationPricePrHour},{ParkingSpotID}";
        }

        /// <summary>
        /// Restore a previous state of the <see cref="ParkingTicket"/> based on the passed in <typeparamref name="SaveType"/> <see langword="value"/>
        /// </summary>
        /// <param name="_data"></param>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>An <see cref="Array"/> of <see cref="KeyValuePair{TKey, TValue}"/> <see langword="objects"/> where <strong>key</strong> is the property name in <i>PascalCase</i></returns>
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

        /// <summary>
        /// Set the <see langword="value"/> of a property
        /// </summary>
        /// <param name="_propertyName">The property name in <i>PascalCase</i></param>
        /// <param name="_value">The <see langword="value"/> to assign the property</param>
        public virtual void SetProperty (string _propertyName, object _value)
        {
            object property = null;
            try
            {
                switch ( _propertyName )
                {
                    case "ParkingSpotID":
                        property = ParkingSpotID;
                        ParkingSpotID = ( int ) _value;
                        break;
                    case "OccupationStamp":
                        property = OccupationStamp;
                        OccupationStamp = ( DateTime ) _value;
                        break;
                    case "OccupationPricePrHour":
                        property = OccupationPricePrHour;
                        OccupationPricePrHour = ( decimal ) _value;
                        break;
                    default:
                        throw new PropertyNotFoundException<IMyParkingTicket>(GetTicketProperties());
                }
            }
            catch ( InvalidCastException _e )
            {
                throw new InvalidCastException($"Invalid Property Value: type of ({property})<{property.GetType()}> is not equal to type of ({_value})<{_value.GetType()}>", _e);
            }

        }
    }
}