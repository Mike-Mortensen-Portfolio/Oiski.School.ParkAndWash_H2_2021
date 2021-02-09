using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines an <see cref="IMyTicket"/> for an <see cref="Parking.IMyParkingSpot"/> that includes a service check
    /// </summary>
    internal class ParkingServiceTicket : ParkingTicket
    {
        /// <summary>
        /// Creates a new instance of type <see cref="ParkingServiceTicket"/> where the <paramref name="_parkingSpotID"/>, <paramref name="_pricePrHour"/> and <paramref name="serviceType"/> is set
        /// </summary>
        /// <param name="_parkingSpotID"></param>
        /// <param name="_pricePrHour"></param>
        /// <param name="serviceType"></param>
        public ParkingServiceTicket ( int _parkingSpotID, decimal _pricePrHour, string serviceType ) : base (_parkingSpotID, _pricePrHour)
        {
            ServiceType = serviceType;
            TicketType = typeof (ParkingServiceTicket);
        }

        /// <summary>
        /// The type of service check this <see cref="ParkingServiceTicket"/> includes
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Save the current state of the <see cref="ParkingServiceTicket"/>
        /// </summary>
        /// <returns>The current state of <see langword="this"/> <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> as an instance of type <typeparamref name="SaveType"/></returns>
        public override string SaveEntity ()
        {
            return $"{base.SaveEntity ()},{ServiceType}";
        }

        /// <summary>
        /// Restore a previous state of the <see cref="ParkingServiceTicket"/> based on the passed in <typeparamref name="SaveType"/> <see langword="value"/>
        /// </summary>
        /// <param name="_data"></param>
        /// <exception cref="System.IO.InvalidDataException"
        public override void BuildEntity ( string _values )
        {
            base.BuildEntity (_values);

            string[] values = _values.Split (",");

            ServiceType = values[ 0 ];
        }

        /// <summary>
        /// Gets a collection of <see cref="KeyValuePair{TKey, TValue}"/> that represents the properties that are available to the <see cref="IMyPropertyAccessor"/> <see langword="interface"/>
        /// </summary>
        /// <returns>An <see cref="Array"/> of <see cref="KeyValuePair{TKey, TValue}"/> <see langword="objects"/> where <strong>key</strong> is the property name in <i>PascalCase</i></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArrayTypeMismatchException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="RankException"></exception>
        public override KeyValuePair<string, object>[] GetProperties ()
        {
            KeyValuePair<string, object>[] properties = new KeyValuePair<string, object>[ 4 ];

            base.GetProperties ().CopyTo (properties, 0);
            properties[ 3 ] = KeyValuePair.Create ("ChargeCostPrKWH", ( object ) ServiceType);

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
                base.SetProperty (_propertyName, _value);
            }
            catch ( PropertyNotFoundException<IMyParkingTicket> _propertyException )
            {
                try
                {
                    switch ( _propertyName )
                    {
                        case "ChargeCostPrKWH":
                            property = ServiceType;
                            ServiceType = ( string ) _value;
                            break;
                        default:
                            throw _propertyException;
                    }
                }
                catch ( InvalidCastException _invalidException )
                {
                    throw new InvalidCastException ($"Invalid Property Value: type of ({property})<{property.GetType ()}> is not equal to type of ({_value})<{_value.GetType ()}>", _invalidException);
                }
            }
        }
    }
}
