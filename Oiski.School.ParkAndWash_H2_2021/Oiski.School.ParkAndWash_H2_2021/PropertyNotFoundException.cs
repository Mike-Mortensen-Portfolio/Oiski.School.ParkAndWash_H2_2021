using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// The exception that is thrown when a property wasn't found
    /// </summary>
    public class PropertyNotFoundException<T> : Exception
    {
        public override string Message { get; } = $"Property doesn't exist!{Environment.NewLine}Properties for {typeof(T)}:{Environment.NewLine}";
        /// <summary>
        /// The collection of <see cref="KeyValuePair{TKey, TValue}"/> that contains the properties for the particular <typeparamref name="T"/> type
        /// </summary>
        public KeyValuePair<string, object>[] Properties { get; }

        /// <summary>
        /// Creates a new instance of type <see cref="PropertyNotFoundException{T}"/>
        /// </summary>
        /// <param name="_properties">The collection of properties associated with the type <typeparamref name="T"/></param>
        public PropertyNotFoundException (KeyValuePair<string, object>[] _properties)
        {
            Properties = _properties;

            foreach ( KeyValuePair<string, object> property in _properties )
            {
                Message += $"{property.Key}: {property.Value}{Environment.NewLine}";
            }
        }

        /// <summary>
        /// Creates a new instance of type <see cref="PropertyNotFoundException{T}"/> where <paramref name="_message"/> is set
        /// </summary>
        /// <param name="_message">The error message to provide when the <see cref="Exception"/> is thrown</param>
        /// <param name="_properties">The collection of properties associated with the type <typeparamref name="T"/></param>
        public PropertyNotFoundException (string _message, KeyValuePair<string, object>[] _properties)
        {
            Properties = _properties;
            Message = _message;
        }
    }
}
