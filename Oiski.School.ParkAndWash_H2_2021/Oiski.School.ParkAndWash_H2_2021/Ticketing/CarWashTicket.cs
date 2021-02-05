using Oiski.School.ParkAndWash_H2_2021.Washing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines a basic ticket for an <see cref="IMyCarWash"/>
    /// </summary>
    internal class CarWashTicket : Ticket, IMyCarWashTicket, IMyRepositoryEntity<int, string>
    {
        /// <summary>
        /// Initialize a new instance of type <see cref="CarWashTicket"/>
        /// </summary>
        internal CarWashTicket () : base ()
        {
            TicketType = typeof (CarWashTicket);
        }

        /// <summary>
        /// Initialize a new instance of type <see cref="CarWash"/> with an attached <see cref="IMyCarWash"/> and a wash price
        /// </summary>
        /// <param name="_carWashID">The ID of the <see cref="IMyCarWash"/> <see langword="object"/> to attach to this ticket</param>
        /// <param name="_washPrice">The price for this wash</param>
        public CarWashTicket ( int _carWashID, decimal _washPrice ) : base ()
        {
            CarWashID = _carWashID;
            WashPrice = _washPrice;
            TicketType = typeof (CarWashTicket);
        }

        /// <summary>
        /// Initialize a new instance of type <see cref="CarWash"/> of a specific type, with an attached <see cref="IMyCarWash"/> and a wash price
        /// </summary>
        /// <param name="_carWashID">The ID of the <see cref="IMyCarWash"/> <see langword="object"/> to attach to this ticket</param>
        /// <param name="_washPrice">The price for this wash</param>
        /// <param name="_type">The type of wash the ticket represents</param>
        public CarWashTicket ( int _carWashID, decimal _washPrice, CarWashType _type )
        {
            CarWashID = _carWashID;
            WashPrice = _washPrice;
            WashType = _type;
            TicketType = typeof (CarWashTicket);
        }

        /// <summary>
        /// The ID of the <see cref="IMyCarWash"/> that is attached to this <see cref="CarWashTicket"/>
        /// </summary>
        public int CarWashID { get; set; }
        /// <summary>
        /// The type of wash this ticket represents
        /// </summary>
        public CarWashType WashType { get; set; }
        /// <summary>
        /// The price for the wash
        /// </summary>
        public decimal WashPrice { get; set; }

        /// <summary>
        /// Restore a previous state of the <see cref="CarWashTicket"/> based on the passed in <typeparamref name="SaveType"/> <see langword="value"/>
        /// </summary>
        /// <param name="_data"></param>
        /// <exception cref="InvalidDataException"></exception>
        public void BuildEntity ( string _data )
        {
            string[] values = _data.Split (",");

            if ( int.TryParse (values[ 1 ].Replace ("ID", string.Empty), out int _id) && int.TryParse (values[ 2 ], out int _type) && decimal.TryParse (values[ 3 ], out decimal _price) )
            {
                this.ID = _id;
                WashType = ( CarWashType ) _type;
                WashPrice = _price;
            }
            else
            {
                throw new InvalidDataException ($"One or more fields couldn't be retrieved from: {_data}");
            }
        }

        /// <summary>
        /// Gets a collection of <see cref="KeyValuePair{TKey, TValue}"/> that represents the properties that are available to the <see cref="IMyPropertyAccessor"/> <see langword="interface"/>
        /// </summary>
        /// <returns>An <see cref="Array"/> of <see cref="KeyValuePair{TKey, TValue}"/> <see langword="objects"/> where <strong>key</strong> is the property name in <i>PascalCase</i></returns>
        public override KeyValuePair<string, object>[] GetTicketProperties ()
        {
            KeyValuePair<string, object>[] properties = new KeyValuePair<string, object>[ 3 ];

            properties[ 0 ] = KeyValuePair.Create ("CarWashID", ( object ) CarWashID);
            properties[ 1 ] = KeyValuePair.Create ("WashType", ( object ) WashType);
            properties[ 2 ] = KeyValuePair.Create ("WashPrice", ( object ) WashPrice);

            return properties;
        }

        /// <summary>
        /// Save the current state of the <see cref="CarWashTicket"/>
        /// </summary>
        /// <returns>The current state of <see langword="this"/> <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> as an instance of type <typeparamref name="SaveType"/></returns>
        public string SaveEntity ()
        {
            return $"Type{TicketType.FullName},ID{ID},{ CarWashID},{( int ) WashType},{WashPrice}";
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
                    case "CarWashID":
                        property = CarWashID;
                        CarWashID = ( int ) _value;
                        break;
                    case "WashType":
                        property = WashType;
                        WashType = ( CarWashType ) _value;
                        break;
                    case "WashPrice":
                        property = WashPrice;
                        WashPrice = ( decimal ) _value;
                        break;
                    default:
                        throw new PropertyNotFoundException<IMyParkingTicket> (GetTicketProperties ());
                }
            }
            catch ( InvalidCastException _invalidException )
            {
                throw new InvalidCastException ($"Invalid Property Value: type of ({property})<{property.GetType ()}> is not equal to type of ({_value})<{_value.GetType ()}>", _invalidException);
            }
        }
    }
}
