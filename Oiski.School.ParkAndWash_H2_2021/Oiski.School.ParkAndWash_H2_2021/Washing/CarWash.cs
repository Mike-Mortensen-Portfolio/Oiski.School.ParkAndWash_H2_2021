using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Oiski.School.ParkAndWash_H2_2021.Washing
{
    /// <summary>
    /// Defines the <see langword="base"/> type for any <see cref="IMyCarWash"/> <see langword="object"/>
    /// </summary>
    internal class CarWash : IMyCarWash, IMyRepositoryEntity<int, string>
    {
        private static int washCount = 0;

        /// <summary>
        /// Initializes a new instance of type <see cref="CarWash"/>
        /// </summary>
        public CarWash ()
        {
            ID = ++washCount;
            Name = $"Car Wash {ID}";
            Rutine = new CarWashState[] { CarWashState.Completed };
        }

        public CarWash ( string _name )
        {
            ID = ++washCount;
            CancelSource = new CancellationTokenSource ();
            CancelToken = CancelSource.Token;
            Name = _name;
            Rutine = new CarWashState[] { CarWashState.Completed };
        }

        public CarWash ( string _name, CarWashState[] _rutine )
        {
            ID = ++washCount;
            CancelSource = new CancellationTokenSource ();
            CancelToken = CancelSource.Token;
            Name = _name;
            Rutine = _rutine;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// The amount of time the <see cref="CarWash"/> has been used
        /// </summary>
        public int TimesRun { get; private set; }
        /// <summary>
        /// Returns <see langword="true"/> if the washing process is running; Otherwise, <see langword="false"/>
        /// </summary>
        public bool IsRunning { get; set; }
        /// <summary>
        /// How far the washing process has come, represented as percentage 
        /// </summary>
        public double ProcessProgress
        {
            get
            {
                return currentTickCount / totalWashDuration * 100;
            }
        }
        private double totalWashDuration = 0;
        private double currentTickCount = 0;

        /// <summary>
        /// The current <see cref="CarWashState"/> that defines which process, if any, the <see cref="CarWash"/> is currently doing
        /// </summary>
        public CarWashState State { get; set; } = CarWashState.NotRunning;

        /// <summary>
        /// The cancellation source method
        /// </summary>
        protected virtual CancellationTokenSource CancelSource { get; private set; }
        /// <summary>
        /// The cancellation token retrived from the <see cref="CancelSource"/> <see langword="object"/>
        /// </summary>
        protected virtual CancellationToken CancelToken { get; private set; }

        /// <summary>
        /// The type of wash process the <see cref="CarWash"/> will undergo
        /// </summary>
        public CarWashState[] Rutine { get; set; }

        /// <summary>
        /// Rebuild <see langword="this"/> instance based on the passed in <see langword="string"/> <paramref name="_values"/>
        /// </summary>
        /// <param name="_values"></param>
        /// <exception cref="InvalidDataException"></exception>
        public void BuildEntity ( string _data )
        {
            string[] values = _data.Split (",");

            if ( int.TryParse (values[ 0 ].Replace ("ID", string.Empty), out int _id) && int.TryParse (values[ 2 ], out int _timesRun) )
            {
                ID = _id;
                Name = values[ 1 ];
                TimesRun = _timesRun;
            }
        }

        /// <summary>
        /// Abort the washing progress. (<strong>Note: </strong> <i>This will not guarantee that the process is aborted right away! Check <see cref="State"/> to ensure that the process was indeed canceled</i>)
        /// </summary>
        public virtual void AbortWash ()
        {
            if ( CancelSource != null )
            {
                CancelSource.Cancel ();

                CancelSource = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A <see langword="string"/> containing each property value for <see langword="this"/> instance, seperated by comma</returns>
        public string SaveEntity ()
        {
            return $"ID{ID},{Name},{TimesRun}";
        }

        /// <summary>
        /// Begin the washing process
        /// </summary>
        /// <exception cref="CarWashRunningException"></exception>
        public async void StartWashAsync ()
        {
            if ( !IsRunning )
            {
                CancelSource = new CancellationTokenSource ();
                CancelToken = CancelSource.Token;

                IsRunning = true;
                foreach ( CarWashState state in Rutine )
                {
                    totalWashDuration += TimeSpan.FromMilliseconds (( int ) state).TotalSeconds;
                }
                currentTickCount = 0;
                TimesRun++;
                State = CarWashState.NotRunning;
                await RunWashProcess ();
                IsRunning = false;
            }
            else
            {
                throw new CarWashRunningException ($"Car Wash with ID: {ID} is already running!", State, ProcessProgress);
            }
        }

        /// <summary>
        /// Simulate a car wash process
        /// </summary>
        /// <returns></returns>
        protected Task RunWashProcess ()
        {
            return Task.Factory.StartNew (() =>
            {
                int index = 0;
                CarWashState trueState = State;
                while ( trueState != CarWashState.Completed )
                {

                    if ( CheckIfShouldAbort () )
                    {
                        return;
                    }

                    if ( !CancelSource.IsCancellationRequested && trueState == CarWashState.Proceeding || State == CarWashState.NotRunning )
                    {
                        trueState = Process (Rutine[ index++ ]);
                    }
                }
            });
        }

        /// <summary>
        /// Simulates a car wash process. The process is defined by the <see cref="CarWashState"/> <paramref name="_process"/>
        /// </summary>
        /// <param name="_process"></param>
        /// <returns></returns>
        protected virtual CarWashState Process ( CarWashState _process )
        {
            State = _process;

            #region Caclulate Process
            double seconds = TimeSpan.FromMilliseconds (( int ) _process).TotalSeconds;
            double tick = 0;
            do
            {
                Thread.Sleep (1000);
                tick++;
                currentTickCount++;

            } while ( tick < seconds );
            #endregion

            if ( _process == CarWashState.Drying )
            {
                State = CarWashState.Completed;
                return CarWashState.Completed;
            }

            return CarWashState.Proceeding;
        }

        /// <summary>
        /// Will check if a cancellation request has been thrown and aborts the wash process if so
        /// </summary>
        protected virtual bool CheckIfShouldAbort ()
        {
            try
            {
                CancelToken.ThrowIfCancellationRequested ();
            }
            catch ( OperationCanceledException )
            {

                State = CarWashState.Aborted;
                IsRunning = false;
                return true;
            }

            return false;
        }
    }
}
