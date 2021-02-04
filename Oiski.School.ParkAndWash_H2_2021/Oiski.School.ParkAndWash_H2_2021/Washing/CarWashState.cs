using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Washing
{
    /// <summary>
    /// Defines a set of possible states a <see cref="IMyCarWash"/> can have
    /// </summary>
    public enum CarWashState
    {
        /// <summary>
        /// The state an <see cref="IMyCarWash"/> is in before the wash process has been started
        /// </summary>
        NotRunning,
        /// <summary>
        /// The state an <see cref="IMyCarWash"/> is in when the wash process has been paused. (<i>This could be due to an error</i>)
        /// </summary>
        Stopped,
        /// <summary>
        /// The state an <see cref="IMyCarWash"/> is in when the wash process has been canceled
        /// </summary>
        Aborted,
        /// <summary>
        /// The state an <see cref="IMyCarWash"/> is in when the wash process has been completed succesfully
        /// </summary>
        Completed,
        /// <summary>
        /// The state an <see cref="IMyCarWash"/> is in right after a wash stage has been completed and right before another stage is being initiated
        /// </summary>
        Proceeding,
        /// <summary>
        /// The first stage of the wash process
        /// </summary>
        Soaping = 5000,
        /// <summary>
        /// The second stage of the wash process
        /// </summary>
        Scrubbing = 10000,
        /// <summary>
        /// The thirs stage of the wash process
        /// </summary>
        Blasting = 2000,
        /// <summary>
        /// The fourth stage of the wash process
        /// </summary>
        Rinsing = 7000,
        /// <summary>
        /// The Fifth stage of the wash process
        /// </summary>
        Waxing = 12000,
        /// <summary>
        /// The last stage in any wash process
        /// </summary>
        Drying = 15000
    }
}
