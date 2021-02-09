using Oiski.Common.Files;
using Oiski.School.ParkAndWash_H2_2021.Parking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    /// <summary>
    /// The <see cref="IMyRepository{EntityType, SaveType}"/> <see langword="object"/> that serves as a link between <see cref="IMyParkingSpot"/> <strong>datastorage</strong> and the program
    /// </summary>
    public sealed class ParkingRepository : IMyRepository<IMyParkingSpot, string>
    {
        /// <summary>
        /// Creates a new instance of type <see cref="ParkingRepository"/>
        /// </summary>
        private ParkingRepository ()
        {
            file = new FileHandler (filePath);
        }

        private static ParkingRepository link = null;
        private FileHandler file;

        /// <summary>
        /// The entry point for the <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> 
        /// </summary>
        public static ParkingRepository Link
        {
            get
            {
                if ( link == null )
                {
                    link = new ParkingRepository ();
                }

                return link;
            }
        }

        private readonly string filePath = $"{Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location)}\\ParkingSpots.csv";

        /// <summary>
        /// Delete an <see cref="IMyParkingSpot"/> entry in <strong>data storage</strong>
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name = "_identifier"> The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyParkingSpot"/> <see langword="object"/></param >
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
        public bool DeleteData<IDType> ( IMyRepositoryEntity<IDType, string> _entity )
        {
            if ( GetDataByIdentifier (_entity.ID) != null )
            {
                file.DeleteLine (file.GetLineNumber (file.FindLine (_entity.ID.ToString ())));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find and return the first occurence that matches the <typeparamref name="IDType"/> <paramref name="_identifier"/>
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_identifier">The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyParkingSpot"/> <see langword="object"/></param>
        /// <returns>The first <see cref="IMyParkingSpot"/> attached to the <typeparamref name="IDType"/> <paramref name="_identifier"/>. If no <see cref="IMyParkingSpot"/> was found this will return <see langword="null"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        public IMyParkingSpot GetDataByIdentifier<IDType> ( IDType _id )
        {
            string data = file.FindLine ($"ID{Common.Generics.Converter.CastGeneric<IDType, int> (_id)}");

            if ( data != null )
            {
                IMyParkingSpot spot = Factory.CreateDefaultParkingSpot ();
                ( ( IMyRepositoryEntity<int, string> ) spot ).BuildEntity (data);
                return spot;
            }

            return null;
        }

        /// <summary>
        /// Collects all the <see cref="IMyParkingSpot"/> entries in <strong>data storage</strong>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> where T is <see cref="IMyParkingSpot"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        public IEnumerable<IMyParkingSpot> GetEnumerable ()
        {
            List<IMyParkingSpot> spots = new List<IMyParkingSpot> ();

            foreach ( string data in file.ReadLines () )
            {
                if ( !string.IsNullOrEmpty (data) )
                {
                    IMyRepositoryEntity<int, string> spot = Factory.CreateDefaultParkingSpot () as IMyRepositoryEntity<int, string>;
                    spot.BuildEntity (data);

                    spots.Add (spot as IMyParkingSpot);
                }
            }

            return spots;
        }

        /// <summary>
        /// Insert an <see cref="IMyParkingSpot"/> data entry into the <strong>datastorage</strong>
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
        public bool InsertData<IDType> ( IMyRepositoryEntity<IDType, string> _data )
        {
            if ( GetDataByIdentifier (_data.ID) == null )
            {
                file.WriteLine (_data.SaveEntity (), true);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Update a record in the <see cref="IMyParkingSpot"/> <strong>data storage</strong>
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
        public bool UpdateData<IDType> ( IMyRepositoryEntity<IDType, string> _data )
        {
            if ( !string.IsNullOrWhiteSpace (file.FindLine ($"ID{_data.ID}")) )
            {
                file.UpdateLine (_data.SaveEntity (), file.GetLineNumber (file.FindLine ($"ID{Common.Generics.Converter.CastGeneric<IDType, int> (_data.ID)}")));
                return true;
            }

            return false;
        }
    }
}