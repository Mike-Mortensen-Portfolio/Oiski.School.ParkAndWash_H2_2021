using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines an <see cref="IMyTicket"/> for an <see cref="Parking.IMyParkingSpot"/> with a charge station
    /// </summary>
    internal class ParkingChargeTicket : ParkingTicket
    {
        /// <summary>
        /// Creates a new instance of type <see cref="ParkingChargeTicket"/> where the <paramref name="_parkingSpotID"/>, <paramref name="_pricePrHour"/> and <paramref name="_chargeCostPrKWH"/> is set
        /// </summary>
        /// <param name="_parkingSpotID"></param>
        /// <param name="_pricePrHour"></param>
        /// <param name="_chargeCostPrKWH"></param>
        public ParkingChargeTicket ( int _parkingSpotID, decimal _pricePrHour, decimal _chargeCostPrKWH ) : base (_parkingSpotID, _pricePrHour)
        {
            ChargeCostPrKWH = _chargeCostPrKWH;
            TicketType = typeof (ParkingChargeTicket);
        }

        /// <summary>
        /// The cost pr. Kwh while charging
        /// </summary>
        public decimal ChargeCostPrKWH { get; set; }
        /// <summary>
        /// The amount of Kw that was charged
        /// </summary>
        public double ChargedKWatt { get; set; }

        /// <summary>
        /// Save the current state of the <see cref="ParkingChargeTicket"/>
        /// </summary>
        /// <returns>The current state of <see langword="this"/> <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> as an instance of type <typeparamref name="SaveType"/></returns>
        public override string SaveEntity ()
        {
            return $"{base.SaveEntity ()},{ChargeCostPrKWH},{ChargedKWatt}";
        }

        /// <summary>
        /// Restore a previous state of the <see cref="ParkingChargeTicket"/> based on the passed in <typeparamref name="SaveType"/> <see langword="value"/>
        /// </summary>
        /// <param name="_data"></param>
        /// <exception cref="InvalidDataException"></exception>
        public override void BuildEntity ( string _values )
        {
            base.BuildEntity (_values);

            string[] values = _values.Split (",");
            if ( decimal.TryParse (values[ 4 ].ToString (), out decimal _costPrKWH) && double.TryParse (values[ 5 ].ToString (), out double _chargedKWatt) )
            {
                ChargeCostPrKWH = _costPrKWH;
                ChargedKWatt = _chargedKWatt;
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
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArrayTypeMismatchException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="RankException"></exception>
        public override KeyValuePair<string, object>[] GetTicketProperties ()
        {
            int basePropertyCount = base.GetTicketProperties ().Length;

            KeyValuePair<string, object>[] properties = new KeyValuePair<string, object>[ basePropertyCount + 2 ];

            base.GetTicketProperties ().CopyTo (properties, 0);
            properties[ basePropertyCount - 2 ] = KeyValuePair.Create ("ChargeCostPrKWH", ( object ) ParkingSpotID);
            properties[ basePropertyCount - 1 ] = KeyValuePair.Create ("ChargedKWatt", ( object ) OccupationStamp);

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
                            property = ChargeCostPrKWH;
                            ChargeCostPrKWH = ( decimal ) _value;
                            break;
                        case "ChargedKWatt":
                            property = ChargedKWatt;
                            ChargedKWatt = ( double ) _value;
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
