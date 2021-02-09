using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines a basic <see cref="IMyTicket"/> for an <see cref="Parking.IMyParkingSpot"/>
    /// </summary>
    internal class ParkingTicket : Ticket, IMyParkingTicket, IMyRepositoryEntity<int, string>
    {
        /// <summary>
        /// Creates a new instance of type <see cref="ParkingTicket"/>
        /// </summary>
        internal ParkingTicket () : base ()
        {
            TicketType = typeof (ParkingTicket);
        }

        /// <summary>
        /// Creates a new instance of type <see cref="ParkingTicket"/> where the <paramref name="_parkingSpotID"/> and <paramref name="_pricePrHour"/> is set
        /// </summary>
        /// <param name="_parkingSpotID"></param>
        /// <param name="_pricePrHour"></param>
        /// <exception cref="OverflowException"></exception>
        public ParkingTicket ( int _parkingSpotID, decimal _pricePrHour ) : base ()
        {
            ParkingSpotID = _parkingSpotID;
            OccupationStamp = DateTime.Now;
            OccupationPricePrHour = _pricePrHour;
            TicketType = typeof (ParkingTicket);
        }

        /// <summary>
        /// The ID of the <see cref="Parking.IMyParkingSpot"/> attached to this <see cref="IMyTicket"/>
        /// </summary>
        public int ParkingSpotID { get; set; }
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
            return $"Type{TicketType.FullName},ID{ID},{OccupationStamp},{OccupationPricePrHour.ToString (CultureInfo.CreateSpecificCulture ("en-GB"))},{ParkingSpotID}";
        }

        /// <summary>
        /// Restore a previous state of the <see cref="ParkingTicket"/> based on the passed in <typeparamref name="SaveType"/> <see langword="value"/>
        /// </summary>
        /// <param name="_data"></param>
        /// <exception cref="InvalidDataException"></exception>
        public virtual void BuildEntity ( string _values )
        {
            string[] values = _values.Split (",");

            if ( int.TryParse (values[ 1 ].Replace ("ID", string.Empty), out int _id) && DateTime.TryParse (values[ 2 ], out DateTime _stamp) && decimal.TryParse (values[ 3 ], out decimal _spotFee) && int.TryParse (values[ 4 ], out int _spotID) )
            {
                this.ID = _id;
                this.OccupationStamp = _stamp;
                this.OccupationPricePrHour = _spotFee;
                this.ParkingSpotID = _spotID;
            }
            else
            {
                throw new InvalidDataException ($"One or more fields couldn't be retrieved from: {_values}");
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="KeyValuePair{TKey, TValue}"/> that represents the properties that are available to the <see cref="IMyPropertyAccessor"/> <see langword="interface"/>
        /// </summary>
        /// <returns>An <see cref="Array"/> of <see cref="KeyValuePair{TKey, TValue}"/> <see langword="objects"/> where <strong>key</strong> is the property name in <i>PascalCase</i></returns>
        public override KeyValuePair<string, object>[] GetProperties ()
        {
            KeyValuePair<string, object>[] properties = new KeyValuePair<string, object>[ 3 ];

            properties[ 0 ] = KeyValuePair.Create ("ParkingSpotID", ( object ) ParkingSpotID);
            properties[ 1 ] = KeyValuePair.Create ("OccupationStamp", ( object ) OccupationStamp);
            properties[ 2 ] = KeyValuePair.Create ("OccupationPricePrHour", ( object ) OccupationPricePrHour);

            return properties;
        }

        /// <summary>
        /// Set the <see langword="value"/> of a property
        /// </summary>
        /// <param name="_propertyName">The property name in <i>PascalCase</i></param>
        /// <param name="_value">The <see langword="value"/> to assign the property</param>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="PropertyNotFoundException{T}"></exception>
        public override void SetProperty ( string _propertyName, object _value )
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
                        throw new PropertyNotFoundException<IMyParkingTicket> (GetProperties ());
                }
            }
            catch ( InvalidCastException _invalidException )
            {
                throw new InvalidCastException ($"Invalid Property Value: type of ({property})<{property.GetType ()}> is not equal to type of ({_value})<{_value.GetType ()}>", _invalidException);
            }
        }
    }
}