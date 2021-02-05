using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oiski.School.ParkAndWash_H2_2021.Washing
{
    /// <summary>
    /// Represents a premium car wash
    /// </summary>
    internal class GoldWash : CarWash
    {
        /// <summary>
        /// Initialize a new instance of type <see cref="GoldWash"/>
        /// </summary>
        public GoldWash ()
        {

        }

        /// <summary>
        /// Initializes a new instance of type <see cref="GoldWash"/> where the name is set
        /// </summary>
        public GoldWash ( string _name ) : base ()
        {
            Name = _name;
        }

        public override CarWashType Type { get; } = CarWashType.Gold;

        /// <summary>
        /// Simulate a car wash that includes rinsing and waxing
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
                    }
                }
            });
        }
    }
}
