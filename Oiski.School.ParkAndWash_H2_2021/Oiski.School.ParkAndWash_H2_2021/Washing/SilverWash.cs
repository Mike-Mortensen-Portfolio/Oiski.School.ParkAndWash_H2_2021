using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oiski.School.ParkAndWash_H2_2021.Washing
{
    /// <summary>
    /// Represents an extended car wash
    /// </summary>
    internal class SilverWash : CarWash
    {
        /// <summary>
        /// Initialize a new instance of type <see cref="SilverWash"/>
        /// </summary>
        public SilverWash ()
        {

        }

        /// <summary>
        /// Initializes a new instance of type <see cref="SilverWash"/> where the name is set
        /// </summary>
        public SilverWash (string _name) : base ()
        {
            Name = _name;
        }

        public override CarWashType Type { get; } = CarWashType.Silver;

        /// <summary>
        /// Simulate a car wash that includes rinsing
        /// </summary>
        /// <returns></returns>
        protected override Task RunWashProcess ()
        {
            return Task.Factory.StartNew (() =>
            {
                CarWashState trueState = State;
                while ( trueState != CarWashState.Completed )
                {
                    CancelToken.ThrowIfCancellationRequested ();

                    if ( trueState == CarWashState.Proceeding || State == CarWashState.NotRunning )
                    {
                        if ( trueState == CarWashState.NotRunning )
                        {
                            State = CarWashState.Soaping - 1;
                        }

                        trueState = Process (++State);

                        if ( State == CarWashState.Rinsing )
                        {
                            State = CarWashState.Waxing;
                        }
                    }
                }
            });
        }
    }
}
