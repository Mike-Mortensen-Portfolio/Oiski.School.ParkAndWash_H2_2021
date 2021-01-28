using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// The <see cref="IMyRepository{T}"/> <see langword="object"/> that serves as a link between <see cref="IMyTicket"/> <strong>data storage</strong> and the program
    /// </summary>
    public sealed class TicketRepository : IMyRepository<IMyTicket>
    {
        /// <summary>
        /// Create a new instance of type <see cref="TicketRepository"/>
        /// </summary>
        private TicketRepository ()
        {
            if ( !File.Exists(filePath) )
            {
                File.Create(filePath).Close();
            }
        }

        private static TicketRepository link = null;

        /// <summary>
        /// The entry point for the <see cref="IMyRepository{T}"/> <see langword="object"/> 
        /// </summary>
        public static TicketRepository Link
        {
            get
            {
                if ( link == null )
                {
                    link = new TicketRepository();
                }

                return link;
            }
        }

        private string filePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Tickets.csv";

        /// <summary>
        /// Delete an <see cref="IMyTicket"/> entry in <strong>data storage</strong>
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name = "_identifier"> The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyTicket"/> <see langword="object"/></param >
        /// <returns></returns>
        public bool DeleteData<IDType> (IDType _identifier)
        {
            if ( GetDataByIdentifier(ParkAndWash.ConvertGeneric<IDType, int>(_identifier)) != null )
            {
                StreamReader file = new StreamReader(filePath);
                string[] entries = file.ReadToEnd().Split(Environment.NewLine);
                file.Close();
                StreamWriter fileWriter = new StreamWriter(filePath);

                string updatedFileContent = string.Empty;

                for ( int i = 0; i < entries.Length; i++ )
                {
                    if ( !entries[i].Contains(ParkAndWash.ConvertGeneric<IDType, int>(_identifier).ToString()) )
                    {
                        updatedFileContent += $"{entries[i]}{Environment.NewLine}";
                    }
                }

                fileWriter.WriteLine(updatedFileContent);
                fileWriter.Close();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Find and return the first occurence that matches the <typeparamref name="IDType"/> <paramref name="_identifier"/>
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_identifier">The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyTicket"/> <see langword="object"/></param>
        /// <returns>The first <see cref="IMyTicket"/> attached to the <typeparamref name="IDType"/> <paramref name="_identifier"/>. If no <see cref="IMyTicket"/> was found this will return <see langword="null"/></returns>
        /// <exception cref="InvalidDataException"></exception>
        public IMyTicket GetDataByIdentifier<IDType> (IDType _identifier)
        {
            using ( StreamReader file = new StreamReader(filePath) )
            {
                string line = file.ReadLine();
                if ( !string.IsNullOrWhiteSpace(line) && line.Contains(ParkAndWash.ConvertGeneric<IDType, int>(_identifier).ToString()) )
                {
                    string[] values = line.Split(",");

                    if ( int.TryParse(values[0], out int _id) && decimal.TryParse(values[1], out decimal _occupationPrHour) && DateTime.TryParse(values[2], out DateTime _occupationStamp) && int.TryParse(values[3], out int _parkingSpotID) )
                    {
                        Ticket ticket = Factory.CreateDefaultTicket() as Ticket;
                        ticket.ID = _id;
                        ticket.OccupationPricePrHour = _occupationPrHour;
                        ticket.OccupationStamp = _occupationStamp;
                        ticket.ParkingSpotID = _parkingSpotID;

                        file.Close();
                        return ticket;
                    }

                    throw new InvalidDataException($"One or more fields couldn't be retrieved from: {line}");
                }
            }

            return null;
        }

        /// <summary>
        /// Collects all the <see cref="IMyTicket"/> entries in <strong>data storage</strong>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> where T is <see cref="IMyTicket"/></returns>
        /// <exception cref="InvalidDataException"></exception>
        public IEnumerable<IMyTicket> GetEnumerable ()
        {
            List<IMyTicket> spots = new List<IMyTicket>();

            using ( StreamReader file = new StreamReader(filePath) )
            {
                string[] values = file.ReadLine().Split(",");

                if ( int.TryParse(values[0], out int _id) && decimal.TryParse(values[1], out decimal _occupationPrHour) && DateTime.TryParse(values[2], out DateTime _occupationStamp) && int.TryParse(values[3], out int _parkingSpotID) )
                {
                    Ticket ticket = Factory.CreateDefaultTicket() as Ticket;
                    ticket.ID = _id;
                    ticket.OccupationPricePrHour = _occupationPrHour;
                    ticket.OccupationStamp = _occupationStamp;
                    ticket.ParkingSpotID = _parkingSpotID;

                    spots.Add(ticket);
                }
                else
                {
                    throw new InvalidDataException($"One or more fields couldn't be retrieved from: {file.ReadLine()}");
                }
            }

            return spots;
        }

        /// <summary>
        /// Insert an <see cref="IMyTicket"/> data entry into the <strong>datastorage</strong>
        /// </summary>
        /// <param name="_data"></param>
        /// <returns><see langword="true"/> if the insertion was successful; Otherwise <see langword="false"/></returns>
        public bool InsertData (IMyTicket _data)
        {
            if ( GetDataByIdentifier(_data.ID) == null )
            {
                using ( StreamWriter file = new StreamWriter(filePath, true) )
                {
                    string lineContent = $"{_data.ID},{_data.OccupationPricePrHour},{_data.OccupationStamp},{_data.ParkingSpotID}";

                    file.WriteLine(lineContent);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Update a record in the <see cref="IMyTicket"/> <strong>data storage</strong>
        /// </summary>
        /// <param name="_data"></param>
        /// <returns><see langword="true"/> if the record was updated successfully; Otherwise <see langword="false"/></returns>
        /// <exception cref="InvalidDataException"></exception>
        public bool UpdateData (IMyTicket _data)
        {
            if ( GetDataByIdentifier(_data.ID) != null )
            {
                using ( StreamReader file = new StreamReader(filePath) )
                {
                    string line = file.ReadLine();

                    if ( line.Contains(_data.ID.ToString()) )
                    {
                        string[] values = line.Split(",");

                        if ( int.TryParse(values[0], out int _id) && bool.TryParse(values[1], out bool _occupied) && decimal.TryParse(values[2], out decimal _spotFee) && int.TryParse(values[3], out int _type) )
                        {
                            string updatedLine = $"{_data.ID},{_data.OccupationPricePrHour},{_data.OccupationStamp},{_data.ParkingSpotID}";

                            string fileContent = file.ReadToEnd();
                            fileContent.Replace(line, updatedLine);

                            using ( StreamWriter writer = new StreamWriter(filePath) )
                            {
                                writer.WriteLine(fileContent);
                            }

                            file.Close();
                            return true;
                        }

                        throw new InvalidDataException($"One or more fields couldn't be retrieved from: {line}");
                    }
                }
            }

            return false;
        }
    }
}