using System;
using System.Runtime.Serialization;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// The <see cref="Exception"/> that is thrown when a duplicate service instance is detected
    /// </summary>
    public class ServiceDuplicateException : Exception
    {
        /// <summary>
        /// Creates a new instance of type <see cref="ServiceDuplicateException"/> where the exception message is set
        /// </summary>
        /// <param name="message"></param>
        public ServiceDuplicateException (string message) : base(message)
        {
        }
    }
}