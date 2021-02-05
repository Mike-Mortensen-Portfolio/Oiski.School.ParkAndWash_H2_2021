using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oiski.School.ParkAndWash_H2_2021.Washing
{
    /// <summary>
    /// Represents a basic form of car wash
    /// </summary>
    internal class BronzeWash : CarWash
    {
        /// <summary>
        /// Initialize a new instance of type <see cref="BronzeWash"/>
        /// </summary>
        public BronzeWash ()
        {

        }

        /// <summary>
        /// Initializes a new instance of type <see cref="BronzeWash"/> where the name is set
        /// </summary>
        public BronzeWash ( string _name ) : base ()
        {
            Name = _name;
        }

        public override CarWashType Type { get; } = CarWashType.Bronze;

        /// <summary>
        /// Simulate a car wash with no extra services
        /// </summary>
        /// <returns></returns>
        protected override Task RunWashProcess ()
        {
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

                            if ( State == CarWashState.Blasting )
                            {
                                State = CarWashState.Waxing;
                            }
                        }
                    }
                });
            }
        }
    }
}
