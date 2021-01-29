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
        public KeyValuePair<string, object>[] Properties { get; }

        public PropertyNotFoundException (KeyValuePair<string, object>[] _properties)
        {
            Properties = _properties;

            foreach ( KeyValuePair<string, object> property in _properties )
            {
                Message += $"{property.Key}: {property.Value}{Environment.NewLine}";
            }
        }

        public PropertyNotFoundException (string _message, KeyValuePair<string, object>[] _properties)
        {
            Properties = _properties;
            Message = _message;
        }
    }
}
