using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oiski.School.ParkAndWash_H2_2021.Parking;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;

namespace Oiski.School.ParkAndWash_H2_2021
{
    public sealed class ParkAndWash : IMyServiceHandler
    {
        private ParkAndWash ()
        {
            services = new List<IMyServiceBase>();
        }

        private static ParkAndWash serviceHandler;
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
        public IReadOnlyList<IMyServiceBase> Services
        {
            get
            {
                return services;
            }
        }

        public IMyServiceBase this[string _serviceID]
        {
            get
            {
                return services.Find(service => service.ServiceID == _serviceID);
            }
        }

        public void InjectService (IMyServiceBase _service)
        {
            //   Inject Service
            throw new NotImplementedException();
        }

        public bool RemoveService (IMyServiceBase _service)
        {
            //   Remove Service
            throw new NotImplementedException();
        }

        public T GetServiceAs<T> (string _serviceID)
        {
            //   Convert base Service to <T>
            throw new NotImplementedException();
        }
    }
}