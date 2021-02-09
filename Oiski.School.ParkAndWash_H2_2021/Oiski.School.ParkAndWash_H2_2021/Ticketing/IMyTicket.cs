using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Defines the <see langword="base"/> form of any ticket
    /// </summary>
    public interface IMyTicket : IMyPropertyAccessor
    {
        int ID { get; }
        Type TicketType { get; }
    }
}