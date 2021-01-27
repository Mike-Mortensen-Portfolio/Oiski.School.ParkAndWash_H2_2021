using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oiski.School.ParkAndWash_H2_2021.Parking;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;

namespace Oiski.School.ParkAndWash_H2_2021
{
    /// <summary>
    /// The entry point for the service reqistration revolved around the Park'N Wash service platform
    /// </summary>
    public sealed class ParkAndWash : IMyServiceHandler
    {
        /// <summary>
        /// Creates a new instance of type <see cref="ParkAndWash"/>
        /// </summary>
        private ParkAndWash ()
        {
            services = new List<IMyServiceBase>();
        }

        private static ParkAndWash serviceHandler;
        /// <summary>
        /// Gets the service handler singleton <see langword="object"/>
        /// </summary>
        public static ParkAndWash ServiceHandler
        {
            get
            {
                if ( serviceHandler == null )
                {
                    serviceHandler = new ParkAndWash();
                }

                return serviceHandler;
            }
        }

        private readonly List<IMyServiceBase> services;
        /// <summary>
        /// The collection of <see cref="IMyServiceBase"/> <see langword="objects"/>
        /// </summary>
        public IReadOnlyList<IMyServiceBase> Services
        {
            get
            {
                return services;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_serviceID">The ID that identifies the <see cref="IMyServiceBase"/> <see langword="object"/></param>
        /// <returns>Returns the first occurence of type <see cref="IMyServiceBase"/> that matches the specified <paramref name="_serviceID"/></returns>
        public IMyServiceBase this[string _serviceID]
        {
            get
            {
                return services.Find(service => service.ServiceID == _serviceID);
            }
        }

        /// <summary>
        /// Inject a <see langword="new"/> <see cref="IMyServiceBase"/> into the <see cref="IMyServiceHandler"/>
        /// </summary>
        /// <param name="_service"></param>
        public void InjectService (IMyServiceBase _service)
        {
            if ( services.Find(service => service.ServiceID == _service.ServiceID) == null )
            {
                services.Add(_service);
            }
            else
            {
                throw new ServiceDuplicateException($"Can't inject service. An ID of {_service.ServiceID} already exists!");
            }
        }

        /// <summary>
        /// Remove a <see cref="IMyServiceBase"/> <see langword="object"/> from the <see cref="IMyServiceHandler"/>
        /// </summary>
        /// <param name="_service"></param>
        /// <returns></returns>
        public bool RemoveService (IMyServiceBase _service)
        {
            IMyServiceBase service = services.Find(service => service.ServiceID == _service.ServiceID);

            return services.Remove(_service);
        }

        /// <summary>
        /// Get an <see cref="IMyServiceBase"/> converted to an instance of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_serviceID"></param>
        /// <returns>The first occurence that matches the <paramref name="_serviceID"/> as an instance of type <typeparamref name="T"/></returns>
        public T GetServiceAs<T> (string _serviceID)
        {
            IMyServiceBase service = services.Find(service => service.ServiceID == _serviceID);
            try
            {
                return ( T ) service;
            }
            catch ( InvalidCastException _e )
            {
                throw new InvalidCastException($"Servicetype mismatch", _e);
            }
        }
    }
}