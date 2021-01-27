using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    internal class TicketService : IMyService<IMyTicket>
    {
        public TicketService ()
        {
            items = new List<IMyTicket>();
        }

        public string ServiceID { get; } = "TicketService";

        private readonly List<IMyTicket> items;
        public IReadOnlyList<IMyTicket> Items
        {
            get
            {
                return items;
            }
        }

        public void AddServiceItem (IMyTicket _item)
        {
            throw new NotImplementedException();
        }

        public bool CancelServiceItem<IDType> (IDType _itemID)
        {
            throw new NotImplementedException();
        }

        public IMyTicket FindServiceItem (Predicate<IMyTicket> _predicate)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IMyTicket> FindAllServiceItems (Predicate<IMyTicket> Predicate)
        {
            throw new NotImplementedException();
        }

        public bool RemoveServiceItem (IMyTicket _item)
        {
            throw new NotImplementedException();
        }

        public IMyTicket RequestServiceItem<ValueType> (ValueType _value)
        {
            throw new NotImplementedException();
        }

        public bool ValidateServiceitem<IDType> (IDType _itemID)
        {
            throw new NotImplementedException();
        }

        public bool ChangeServiceID (string _newID)
        {
            throw new NotImplementedException();
        }
    }
}