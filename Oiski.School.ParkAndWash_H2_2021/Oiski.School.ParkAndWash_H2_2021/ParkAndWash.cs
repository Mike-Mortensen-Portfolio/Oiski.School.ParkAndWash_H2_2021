using System;
using System.Collections.Generic;
using System.Text;
using Oiski.School.ParkAndWash_H2_2021.Parking;
using Oiski.School.ParkAndWash_H2_2021.Customers;

namespace Oiski.School.ParkAndWash_H2_2021
{
    public sealed class ParkAndWash
    {
        private static ParkAndWash service;
        public static ParkAndWash Service
        {
            get
            {
                if ( service == null )
                {
                    service = new ParkAndWash(Factory.CreateParkingHandler(), Factory.CreateTicketService());
                }

                return service;
            }
        }
        public IMyParkingHandler ParkingService { get; }

        public IMyService<IMyTicket> TicketService { get; }

        private ParkAndWash (IMyParkingHandler _parkingHandler, IMyService<IMyTicket> _ticketService)
        {
            ParkingService = _parkingHandler;
            TicketService = _ticketService;
        }
    }
}