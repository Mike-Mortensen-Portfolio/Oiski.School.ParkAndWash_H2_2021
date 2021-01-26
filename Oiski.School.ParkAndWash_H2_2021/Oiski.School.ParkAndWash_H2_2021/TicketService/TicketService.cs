using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.TicketService
{
    internal class TicketService : IMyService<IMyTicket>
    {
        public TicketService ()
        {
            serviceContainer = new List<IMyTicket>();
        }

        private readonly List<IMyTicket> serviceContainer;
        public IReadOnlyList<IMyTicket> ServiceContainer
        {
            get
            {
                return serviceContainer;
            }
        }

        public IMyTicket GenerateServiceItem ()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IMyTicket> GetServiceItemsBy<IDType> (IDType _identifier)
        {
            throw new NotImplementedException();
        }

        public void HandleServiceItem<IDType> (IDType _identifier)
        {
            throw new NotImplementedException();
        }

        public bool ValidateServiceItem<IDType> (IDType _serviceItemID)
        {
            throw new NotImplementedException();
        }
    }
}