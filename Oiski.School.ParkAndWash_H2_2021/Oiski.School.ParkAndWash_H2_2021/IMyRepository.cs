using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// Defines a repository <see langword="object"/> from which data can be pushed to or pulled from a <strong>data storage</strong>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMyRepository<T>
    {
        /// <summary>
        /// Gets the enumerable <see langword="object"/>
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> <see langword="object"/> that can be iteated over</returns>
        IEnumerable<T> GetEnumerable ();
        /// <summary>
        /// Fetch a <typeparamref name="T"/> entry from <strong>data storage</strong>
        /// </summary>
        /// <typeparam name="IDType">The type of the ID of the entry</typeparam>
        /// <param name="_identifier">The ID to search for</param>
        /// <returns>The first occurece of type <typeparamref name="T"/> that matches the <paramref name="_identifier"/></returns>
        T GetDataByIdentifier<IDType> (IDType _identifier);
        /// <summary>
        /// Insert a new <typeparamref name="T"/> entry into the <strong>data storage</strong>
        /// </summary>
        /// <param name="_data"></param>
        /// <returns><see langword="true"/> if the entry could be addedd; Othewise <see langword="false"/></returns>
        bool InsertData (T _data);
        /// <summary>
        /// Delete a <typeparamref name="T"/> entry from the <strong>data storage</strong>
        /// </summary>
        /// <typeparam name="IDType">The type of the ID that identifies the <typeparamref name="T"/> entry</typeparam>
        /// <param name="_identifier">The ID of the entry to delete</param>
        /// <returns><see langword="true"/> if the entry could be deleted; Otherwise <see langword="false"/></returns>
        bool DeleteData<IDType> (IDType _identifier);
        /// <summary>
        /// Update a <typeparamref name="T"/> entry in <strong>data storage</strong>
        /// </summary>
        /// <param name="_data"></param>
        /// <returns><see langword="true"/> if the <typeparamref name="T"/> entry could be updated; Otherwise <see langword="false"/></returns>
        bool UpdateData (T _data);
    }
}