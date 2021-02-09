using Oiski.Common.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Washing
{
    /// <summary>
    /// The <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> that serves as a link between <see cref="IMyCarWash"/> <strong>data storage</strong> and the program
    /// </summary>
    public class CarWashRepository : IMyRepository<IMyCarWash, string>
    {

        /// <summary>
        /// Create a new instance of type <see cref="CarWashRepository"/>
        /// </summary>
        private CarWashRepository ()
        {
            file = new FileHandler (filePath);
        }

        private static CarWashRepository link;
        /// <summary>
        /// The entry point for the <see cref="IMyRepositoryEntity{IDType, SaveType}"/> <see langword="object"/> 
        /// </summary>
        public static CarWashRepository Link
        {
            get
            {
                if ( link == null )
                {
                    link = new CarWashRepository ();
                }
                return link;
            }
        }

        private readonly FileHandler file;
        private readonly string filePath = $"{Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location)}\\CarWashes.csv";

        /// <summary>
        /// Delete an <see cref="IMyCarWash"/> entry in <strong>data storage</strong>
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name = "_identifier"> The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyCarWash"/> <see langword="object"/></param >
        /// <returns><see langword="true"/> if the <see cref="IMyCarWash"/> <see langword="object"/> could be deleted; Otherwise <see langword="false"/></returns>
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
            string data = file.FindLine ($"ID{Common.Generics.Converter.CastGeneric<IDType, int> (_entity.ID)}");

            if ( data != null )
            {
                file.DeleteLine (file.GetLineNumber (data));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find and return the first occurence that matches the <typeparamref name="IDType"/> <paramref name="_identifier"/>
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_identifier">The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyCarWash"/> <see langword="object"/></param>
        /// <returns>The first <see cref="IMyCarWash"/> attached to the <typeparamref name="IDType"/> <paramref name="_identifier"/>. If no <see cref="IMyCarWash"/> was found this will return <see langword="null"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        public IMyCarWash GetDataByIdentifier<IDType> ( IDType _id )
        {
            string data = file.FindLine ($"ID{Common.Generics.Converter.CastGeneric<IDType, int> (_id)}");

            if ( data != null )
            {
                IMyCarWash wash = Factory.CreateDefaultCarWash ();
                ( ( IMyRepositoryEntity<int, string> ) wash ).BuildEntity (data);
                return wash;
            }

            return null;
        }

        /// <summary>
        /// Collects all the <see cref="IMyCarWash"/> entries in <strong>data storage</strong>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> where T is <see cref="IMyCarWash"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        public IEnumerable<IMyCarWash> GetEnumerable ()
        {
            List<IMyCarWash> washes = new List<IMyCarWash> ();

            foreach ( string data in file.ReadLines () )
            {
                if ( data != string.Empty )
                {
                    IMyRepositoryEntity<int, string> wash = Factory.CreateDefaultCarWash () as IMyRepositoryEntity<int, string>;
                    wash.BuildEntity (data);

                    washes.Add (wash as IMyCarWash);
                }
            }

            return washes;
        }

        /// <summary>
        /// Insert an <see cref="IMyCarWash"/> data entry into the <strong>datastorage</strong>
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
            if ( string.IsNullOrWhiteSpace (file.FindLine ($"ID{_data.ID}")) )
            {
                file.WriteLine (_data.SaveEntity (), true);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Update a record in the <see cref="IMyCarWash"/> <strong>data storage</strong>
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
