using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    internal class ParkingWashTicket : ParkingTicket
    {
        public ParkingWashTicket (int _parkingSpotID, decimal _pricePrHour, string _washType) : base(_parkingSpotID, _pricePrHour)
        {
            WashType = _washType;
        }

        public string WashType { get; set; }

        public override string SaveEntity ()
        {
            return $"{base.SaveEntity()},{WashType}";
        }

        public override void BuildEntity (string _values)
        {
            base.BuildEntity(_values);

            string[] values = _values.Split(",");

            WashType = values[0];
        }

        public override KeyValuePair<string, object>[] GetTicketProperties ()
        {
            KeyValuePair<string, object>[] properties = new KeyValuePair<string, object>[4];

            base.GetTicketProperties().CopyTo(properties, 0);
            properties[3] = KeyValuePair.Create("ChargeCostPrKWH", ( object ) WashType);

            return properties;
        }

        public override void SetProperty (string _propertyName, object _value)
        {
            object property = null;

            try
            {
                base.SetProperty(_propertyName, _value);
            }
            catch ( PropertyNotFoundException<IMyParkingTicket> _propertyException )
            {
                try
                {
                    switch ( _propertyName )
                    {
                        case "ChargeCostPrKWH":
                            property = WashType;
                            WashType = ( string ) _value;
                            break;
                        default:
                            throw _propertyException;
                    }
                }
                catch ( InvalidCastException _invalidException )
                {
                    throw new InvalidCastException($"Invalid Property Value: type of ({property})<{property.GetType()}> is not equal to type of ({_value})<{_value.GetType()}>", _invalidException);
                }
            }
        }
    }
}
