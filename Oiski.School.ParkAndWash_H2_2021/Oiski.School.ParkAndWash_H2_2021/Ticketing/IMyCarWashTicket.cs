using Oiski.School.ParkAndWash_H2_2021.Washing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    public interface IMyCarWashTicket : IMyTicket
    {
        int CarWashID { get; }
        CarWashType WashType { get; }
        decimal WashPrice { get; }
    }
}