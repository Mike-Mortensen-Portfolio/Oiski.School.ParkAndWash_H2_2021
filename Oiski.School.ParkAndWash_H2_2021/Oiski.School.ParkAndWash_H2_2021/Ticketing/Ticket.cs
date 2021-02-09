using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// Represents the <see langword="base"/> for any <see cref="IMyTicket"/> instances. (<i>Use this when creating new types of tickets</i>)
    /// </summary>
    internal abstract class Ticket : IMyTicket
    {
        /// <summary>
        /// The amount of tickets created through the <see cref="Ticket"/> <see langword="base"/>
        /// </summary>
        protected static int ticketCount = 0;

        internal Ticket ()
        {
            ID = ++ticketCount;
        }

        public int ID { get; protected set; }
        public Type TicketType { get; protected set; }

        public abstract KeyValuePair<string, object>[] GetProperties ();

        public abstract void SetProperty ( string _propertyName, object _value );
    }
}
