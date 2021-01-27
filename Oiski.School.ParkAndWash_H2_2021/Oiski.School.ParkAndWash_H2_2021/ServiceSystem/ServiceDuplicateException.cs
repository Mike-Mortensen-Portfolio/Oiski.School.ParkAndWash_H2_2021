using System;
using System.Runtime.Serialization;

namespace Oiski.School.ParkAndWash_H2_2021
{
    internal class ServiceDuplicateException : Exception
    {
        public ServiceDuplicateException (string message) : base(message)
        {
        }
    }
}