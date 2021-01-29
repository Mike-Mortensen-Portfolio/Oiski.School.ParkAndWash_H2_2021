using Oiski.School.ParkAndWash_H2_2021.Ticketing;
using System;

namespace Oiski.School.ParkAndWash_H2_2021.Application
{
    class Program
    {
        static void Main (string[] args)
        {
            Console.WriteLine("Hello World!");

            IMyTicket ticket = Factory.CreateParkingTicket(TicketType.ParkingCharge);
            ticket.SetProperty("ID", "1");
        }
    }
}
