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
            base.SetProperty(_propertyName, _value);

            try
            {
                switch ( _propertyName )
                {
                    case "ServiceType":
                        WashType = ( string ) _value;
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
