using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.School.ParkAndWash_H2_2021.Application.Interface;
using Oiski.School.ParkAndWash_H2_2021.Parking;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;
using Oiski.School.ParkAndWash_H2_2021.Washing;
using System.Linq;

namespace Oiski.School.ParkAndWash_H2_2021.Application
{
    class Program
    {
        static void Main ( string[] args )
        {
            #region Setting up Parking Spots

            IMyService<IMyParkingSpot> parkingService = Factory.CreateParkingService ();

            if ( ParkingRepository.Link.GetEnumerable ().ToList ().Count <= 0 )
            {
                IMyParkingSpot spot;
                for ( int i = 0; i < 77; i++ )
                {
                    if ( i >= 0 && i < 5 )
                    {
                        spot = Factory.CreateParkingSpot (SpotType.Handicap);
                        spot.SpotFee = 5M;
                        ParkingRepository.Link.InsertData (spot as IMyRepositoryEntity<int, string>);
                        parkingService.AddServiceItem (spot);
                    }

                    if ( i >= 5 && i < 17 )
                    {
                        spot = Factory.CreateParkingSpot (SpotType.Large);
                        spot.SpotFee = 22M;
                        ParkingRepository.Link.InsertData (spot as IMyRepositoryEntity<int, string>);
                        parkingService.AddServiceItem (spot);
                    }

                    if ( i >= 12 && i < 22 )
                    {
                        spot = Factory.CreateParkingSpot (SpotType.Util);
                        spot.SpotFee = 11M;
                        ParkingRepository.Link.InsertData (spot as IMyRepositoryEntity<int, string>);
                        parkingService.AddServiceItem (spot);
                    }

                    if ( i >= 22 && i < 72 )
                    {
                        spot = Factory.CreateParkingSpot (SpotType.Standard);
                        spot.SpotFee = 18M;
                        ParkingRepository.Link.InsertData (spot as IMyRepositoryEntity<int, string>);
                        parkingService.AddServiceItem (spot);
                    }
                }
            }
            else
            {
                foreach ( IMyParkingSpot spot in ParkingRepository.Link.GetEnumerable () )
                {
                    parkingService.AddServiceItem (spot);
                }
            }

            ParkAndWash.ServiceHandler.InjectService (parkingService);
            #endregion

            #region Setting up Ticket Service
            IMyService<IMyTicket> ticketService = Factory.CreateTicketService ();
            ParkAndWash.ServiceHandler.InjectService (ticketService);
            #endregion

            OiskiEngine.ChangeRenderer (new ColorRenderer ());
            OiskiEngine.Run ();

            MainScreen.Screen.Show ();
        }
    }
}
