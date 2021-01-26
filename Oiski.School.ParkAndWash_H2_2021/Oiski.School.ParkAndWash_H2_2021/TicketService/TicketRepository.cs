using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.TicketService
{
    internal sealed class TicketRepository : IMyRepository<IMyTicket>
    {
        public bool DeleteData<IDType> (IDType _identifier)
        {
            throw new NotImplementedException();
        }

        public IMyTicket GetDataByIdentifier<IDType> (IDType _identifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMyTicket> GetEnumerable ()
        {
            throw new NotImplementedException();
        }

        public bool InsertData (IMyTicket _data)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData (IMyTicket _data)
        {
            throw new NotImplementedException();
        }
    }
}