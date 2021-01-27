using Oiski.School.ParkAndWash_H2_2021.Parking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    public sealed class ParkingRepository : IMyRepository<IMyParkingSpot>
    {
        public bool DeleteData<IDType> (IDType _identifier)
        {
            throw new NotImplementedException();
        }

        public IMyParkingSpot GetDataByIdentifier<IDType> (IDType _identifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMyParkingSpot> GetEnumerable ()
        {
            throw new NotImplementedException();
        }

        public bool InsertData (IMyParkingSpot _data)
        {
            throw new NotImplementedException();
        }

        public bool UpdateData (IMyParkingSpot _data)
        {
            throw new NotImplementedException();
        }
    }
}