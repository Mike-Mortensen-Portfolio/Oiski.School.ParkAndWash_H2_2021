using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    internal class ParkingChargeTicket : ParkingTicket
    {
        public ParkingChargeTicket (int _parkingSpotID, decimal _pricePrHour, decimal _chargeCostPrKWH) : base(_parkingSpotID, _pricePrHour)
        {
            ChargeCostPrKWH = _chargeCostPrKWH;
        }

        public decimal ChargeCostPrKWH { get; set; }
        public double ChargedKWatt { get; set; }

        public override string SaveEntity ()
        {
            return $"{base.SaveEntity()},{ChargeCostPrKWH},{ChargedKWatt}";
        }

        public override void BuildEntity (string _values)
        {
            base.BuildEntity(_values);

            string[] values = _values.Split(",");
            if ( decimal.TryParse(values[4].ToString(), out decimal _costPrKWH) && double.TryParse(values[5].ToString(), out double _chargedKWatt) )
            {
                ChargeCostPrKWH = _costPrKWH;
                ChargedKWatt = _chargedKWatt;
            }
            else
            {
                throw new InvalidDataException($"One or more fields couldn't be retrieved from: {_values}");
            }
        }

        public override KeyValuePair<string, object>[] GetTicketProperties ()
        {
            KeyValuePair<string, object>[] properties = new KeyValuePair<string, object>[5];

            base.GetTicketProperties().CopyTo(properties, 0);
            properties[3] = KeyValuePair.Create("ChargeCostPrKWH", ( object ) ChargeCostPrKWH);
            properties[4] = KeyValuePair.Create("ChargedKWatt", ( object ) ChargedKWatt);

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
                    throw new InvalidCastException($"Invalid Property Value: type of ({property})<{property.GetType()}> is not equal to type of ({_value})<{_value.GetType()}>", _invalidException);
                }
            }
        }
    }
}
