using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    internal class ParkingService : IMyService<IMyParkingSpot>
    {
        public ParkingService ()
        {
            items = new List<IMyParkingSpot>();
        }

        public string ServiceID { get; } = "ParkingService";

        private readonly List<IMyParkingSpot> items;
        public IReadOnlyList<IMyParkingSpot> Items
        {
            get
            {
                return items;
            }
        }

        public void AddServiceItem (IMyParkingSpot _item)
        {
            throw new NotImplementedException();
        }

        public bool CancelServiceItem<IDType> (IDType _itemID)
        {
            throw new NotImplementedException();
        }

        public IMyParkingSpot FindServiceItem (Predicate<IMyParkingSpot> _predicate)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<IMyParkingSpot> FindAllServiceItems (Predicate<IMyParkingSpot> Predicate)
        {
            throw new NotImplementedException();
        }

        public bool RemoveServiceItem (IMyParkingSpot _item)
        {
            throw new NotImplementedException();
        }

        public IMyParkingSpot RequestServiceItem<ValueType> (ValueType _value)
        {
            throw new NotImplementedException();
        }

        public bool ValidateServiceitem<IDType> (IDType _itemID)
        {
            throw new NotImplementedException();
        }

        public bool ChangeServiceID (string _newID)
        {
            throw new NotImplementedException();
        }
    }
}