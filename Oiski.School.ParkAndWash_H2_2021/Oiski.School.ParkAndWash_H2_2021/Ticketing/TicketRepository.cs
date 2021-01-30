using Oiski.Common.Files;
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
            file = new FileHandler(filePath);
        }

        private static TicketRepository link = null;
        private FileHandler file;

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
        /// <returns><see langword="true"/> if the <see cref="IMyParkingSpot"/> <see langword="object"/> could be deleted; Otherwise <see langword="false"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public bool DeleteData<IDType> (IMyRepositoryEntity<IDType, string> _entity)
        {
            if ( GetDataByIdentifier(_entity.ID) != null )
            {
                file.DeleteLine(file.GetLineNumber(file.FindLine(_entity.ID.ToString())));
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
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        public IMyTicket GetDataByIdentifier<IDType> (IDType _id)
        {
            string data = file.FindLine($"ID{Common.Generics.Converter.CastGeneric<IDType, int>(_id)}");

            if ( data != null )
            {
                IMyTicket ticket = Factory.CreateDefaultParkingTicket();
                ( ( IMyRepositoryEntity<int, string> ) ticket ).BuildEntity(data);
                return ticket;
            }

            return null;
        }

        /// <summary>
        /// Collects all the <see cref="IMyTicket"/> entries in <strong>data storage</strong>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> where T is <see cref="IMyTicket"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        public IEnumerable<IMyTicket> GetEnumerable ()
        {
            List<IMyTicket> tickets = new List<IMyTicket>();

            foreach ( string data in file.ReadLines() )
            {
                IMyRepositoryEntity<int, string> ticket = Factory.CreateDefaultParkingTicket() as IMyRepositoryEntity<int, string>;
                ticket.BuildEntity(data);

                tickets.Add(ticket as IMyTicket);
            }

            return tickets;
        }

        /// <summary>
        /// Insert an <see cref="IMyTicket"/> data entry into the <strong>datastorage</strong>
        /// </summary>
        /// <param name="_data"></param>
        /// <returns><see langword="true"/> if the insertion was successful; Otherwise <see langword="false"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public bool InsertData<IDType> (IMyRepositoryEntity<IDType, string> _data)
        {
            if ( GetDataByIdentifier(_data.ID) == null )
            {
                file.WriteLine(_data.SaveEntity(), true);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Update a record in the <see cref="IMyTicket"/> <strong>data storage</strong>
        /// </summary>
        /// <param name="_data"></param>
        /// <returns><see langword="true"/> if the record was updated successfully; Otherwise <see langword="false"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public bool UpdateData<IDType> (IMyRepositoryEntity<IDType, string> _data)
        {
            if ( GetDataByIdentifier(_data.ID) != null )
            {
                file.UpdateLine(_data.SaveEntity(), file.GetLineNumber(file.FindLine($"ID{Common.Generics.Converter.CastGeneric<IDType, int>(_data.ID)}")));
            }

            return false;
        }
    }
}