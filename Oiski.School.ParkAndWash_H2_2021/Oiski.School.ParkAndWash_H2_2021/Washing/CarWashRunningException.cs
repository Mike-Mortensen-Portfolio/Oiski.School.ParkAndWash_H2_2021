using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Washing
{
    /// <summary>
    /// The <see cref="Exception"/> that is thrown if a wash is initiated on an <see cref="IMyCarWash"/> when it's already running
    /// </summary>
    public class CarWashRunningException : Exception
    {
        /// <summary>
        /// Gets the message that describes the current <see cref="Exception"/>
        /// </summary>
        public override string Message { get; }

        /// <summary>
        /// Initializes a new instance of type <see cref="CarWashRunningException"/>
        /// </summary>
        public CarWashRunningException ()
        {
            Message = "Can't begin wash rutine because the car wash is already running";
        }

        /// <summary>
        /// Initializes a new instance of type <see cref="CarWashRunningException"/> with a custom message
        /// </summary>
        /// <param name="_message"></param>
        public CarWashRunningException ( string _message )
        {
            Message = _message;
        }

        /// <summary>
        /// Initializes a new instance of type <see cref="CarWashRunningException"/> with a custom message and details about the current state and process progress of the <see cref="IMyCarWash"/> when the <see cref="Exception"/> was thrown
        /// </summary>
        /// <param name="_message"></param>
        /// <param name="_state"></param>
        /// <param name="_progress"></param>
        public CarWashRunningException ( string _message, CarWashState _state, double _progress )
        {
            Message = $"{_message} - Current State: {_state}. Current process progression: {_progress:0.00}%";
        }
    }
}
