using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oiski.School.ParkAndWash_H2_2021.Washing
{
    /// <summary>
    /// Represents a car wash
    /// </summary>
    public interface IMyCarWash
    {
        int ID { get; }
        string Name { get; set; }
        /// <summary>
        /// Returns <see langword="true"/> if the washing process is running; Otherwise, <see langword="false"/>
        /// </summary>
        bool IsRunning { get; }
        /// <summary>
        /// How far the washing process has come, represented as percentage 
        /// </summary>
        double ProcessProgress { get; }
        /// <summary>
        /// The current <see cref="CarWashState"/> that defines which process, if any, the <see cref="IMyCarWash"/> is currently doing
        /// </summary>
        CarWashState State { get; }
        CarWashType Type { get; }

        /// <summary>
        /// Begin the washing process
        /// </summary>
        void StartWashAsync ();
        /// <summary>
        /// Abort the washing progress. (<strong>Note: </strong> <i>This will not guarantee that the process is aborted right away! Check <see cref="State"/> to ensure that the process was indeed canceled</i>)
        /// </summary>
        void CancelWash ();
    }
}
