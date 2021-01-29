using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    internal class ParkingChargeTicket : ParkingTicket
    {
        public decimal ChargeCostPrKWH { get; set; }
        public double ChargedKWatt { get; set; }
    }
}
