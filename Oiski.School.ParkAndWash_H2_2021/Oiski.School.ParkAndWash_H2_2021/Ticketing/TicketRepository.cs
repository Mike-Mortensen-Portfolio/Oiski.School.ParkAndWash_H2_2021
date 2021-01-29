using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// The <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> that serves as a link between <see cref="IMyTicket"/> <strong>data storage</strong> and the program
    /// </summary>
    public sealed class TicketRepository : IMyRepository<IMyTicket, string>
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
        /// The entry point for the <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> 
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

        private readonly string filePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Tickets.csv";

        /// <summary>
        /// Delete an <see cref="IMyTicket"/> entry in <strong>data storage</strong>
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name = "_identifier"> The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyTicket"/> <see langword="object"/></param >
        /// <returns></returns>
        public bool DeleteData<IDType> (IMyRepositoryEntity<IDType, string> _entity)
        {
            if ( GetDataByIdentifier(_entity) != null )
            {
                StreamReader file = new StreamReader(filePath);
                string[] entries = file.ReadToEnd().Split(Environment.NewLine);
                file.Close();
                StreamWriter fileWriter = new StreamWriter(filePath);

                string updatedFileContent = string.Empty;

                for ( int i = 0; i < entries.Length; i++ )
                {
                    if ( !entries[i].Contains(ParkAndWash.ConvertGeneric<IDType, int>(_entity.ID).ToString()) )
                    {
                        updatedFileContent += $"{entries[i]}";
                    }
                }

                fileWriter.Write(updatedFileContent);
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
        public IMyTicket GetDataByIdentifier<IDType> (IDType _id)
        {
            using ( StreamReader file = new StreamReader(filePath) )
            {
                string line = file.ReadLine();
                if ( !string.IsNullOrWhiteSpace(line) && line.Contains(ParkAndWash.ConvertGeneric<IDType, int>(_id).ToString()) )
                {
                    IMyTicket ticket = Factory.CreateDefaultTicket();
                    ( ( IMyRepositoryEntity<int, string> ) ticket ).BuildEntity(line);
                    return ticket;
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
            List<IMyTicket> tickets = new List<IMyTicket>();

            using ( StreamReader file = new StreamReader(filePath) )
            {
                IMyRepositoryEntity<int, string> ticket = Factory.CreateDefaultTicket() as IMyRepositoryEntity<int, string>;
                ticket.BuildEntity(file.ReadLine());

                tickets.Add(ticket as IMyTicket);
            }

            return tickets;
        }

        /// <summary>
        /// Insert an <see cref="IMyTicket"/> data entry into the <strong>datastorage</strong>
        /// </summary>
        /// <param name="_data"></param>
        /// <returns><see langword="true"/> if the insertion was successful; Otherwise <see langword="false"/></returns>
        public bool InsertData<IDType> (IMyRepositoryEntity<IDType, string> _data)
        {
            if ( GetDataByIdentifier(_data.ID) == null )
            {
                using ( StreamWriter file = new StreamWriter(filePath, true) )
                {
                    string lineContent = _data.SaveEntity();

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
        public bool UpdateData<IDType> (IMyRepositoryEntity<IDType, string> _data)
        {
            if ( GetDataByIdentifier(_data.ID) != null )
            {
                using ( StreamReader file = new StreamReader(filePath) )
                {
                    string line = file.ReadLine();

                    if ( line.Contains(_data.ID.ToString()) )
                    {
                        string updatedLine = _data.SaveEntity();

                        file.Close();
                        StreamReader fullFile = new StreamReader(filePath);
                        string fileContent = fullFile.ReadToEnd();
                        fullFile.Close();
                        fileContent = fileContent.Replace(line, updatedLine);

                        using ( StreamWriter writer = new StreamWriter(filePath) )
                        {
                            writer.Write(fileContent);
                        }

                        return true;
                    }
                }
            }

            return false;
        }
    }
}