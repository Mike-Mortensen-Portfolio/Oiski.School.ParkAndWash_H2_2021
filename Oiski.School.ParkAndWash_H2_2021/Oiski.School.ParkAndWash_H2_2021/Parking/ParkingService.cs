using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Oiski.School.ParkAndWash_H2_2021.Parking
{
    /// <summary>
    /// The service <see langword="object"/> that handles <see cref="IMyParkingSpot"/>s
    /// </summary>
    internal class ParkingService : IMyService<IMyParkingSpot>
    {
        /// <summary>
        /// Creates a new instance of type <see cref="ParkingService"/>
        /// </summary>
        public ParkingService ()
        {
            items = new List<IMyParkingSpot>();
        }

        /// <summary>
        /// The unique identifier for the <see cref="IMyService{T}"/> (<see langword="default"/> is "ParkingService")
        /// </summary>
        public string ServiceID { get; private set; } = "ParkingService";

        private readonly List<IMyParkingSpot> items;
        /// <summary>
        /// The collection of <see cref="IMyParkingSpot"/> <see langword="objects"/>
        /// </summary>
        public IReadOnlyList<IMyParkingSpot> Items
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// Add an <see cref="IMyParkingSpot"/> item to the collection
        /// </summary>
        /// <param name="_item"></param>
        /// <exception cref="ServiceDuplicateException"></exception>
        public void AddServiceItem (IMyParkingSpot _item)
        {
            if ( items.Find(spot => spot.ID == _item.ID) != null )
            {
                items.Add(_item);
            }

            throw new ServiceDuplicateException($"An item with ID: {_item.ID} already exists in Parking Service");
        }

        /// <summary>
        /// Cancel the occupation of a <see cref="IMyParkingSpot"/>
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_itemID">The <see langword="int"/> ID <see langword="value"/></param>
        /// <returns><see langword="true"/> if the <see cref="IMyParkingSpot"/> exists and the occupation could be canceled; Otherwise <see langword="false"/></returns>
        public bool CancelServiceItem<IDType> (IDType _itemID)
        {

            IMyParkingSpot spot = FindServiceItem(spot => spot.ID == ParkAndWash.ConvertGeneric<IDType, int>(_itemID) && spot.Occupied == true);

            if ( spot != null )
            {
                spot.Occupied = false;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Find a <see cref="IMyParkingSpot"/> <see langword="object"/>
        /// </summary>
        /// <param name="_predicate"></param>
        /// <returns>The first occurence that matches the <paramref name="_predicate"/></returns>
        public IMyParkingSpot FindServiceItem (Predicate<IMyParkingSpot> _predicate)
        {
            return items.Find(_predicate);
        }

        /// <summary>
        /// Find all occurences that matches the <paramref name="_predicate"/>
        /// </summary>
        /// <param name="_predicate"></param>
        /// <returns>A collection of <see cref="IMyParkingSpot"/> where each item matches the <paramref name="_predicate"/></returns>
        public IReadOnlyList<IMyParkingSpot> FindAllServiceItems (Predicate<IMyParkingSpot> _predicate)
        {
            return items.FindAll(_predicate);
        }

        /// <summary>
        /// Remove and <see cref="IMyParkingSpot"/> item from the collection
        /// </summary>
        /// <param name="_item"></param>
        /// <returns><see langword="true"/> if the item was found and removed; Otherwise <see langword="false"/></returns>
        public bool RemoveServiceItem (IMyParkingSpot _item)
        {
            IMyParkingSpot spot = items.Find(spot => spot.ID == _item.ID);

            if ( spot != null )
            {
                return items.Remove(spot);
            }

            return false;
        }

        /// <summary>
        /// Request a <see cref="IMyParkingSpot"/> of type <paramref name="_value"/>
        /// </summary>
        /// <typeparam name="ValueType">Must be a <see cref="SpotType"/> <see langword="value"/></typeparam>
        /// <param name="_value">The <see cref="SpotType"/> <see langword="value"/> of the requested <see cref="IMyParkingSpot"/></param>
        /// <returns>An <see cref="IMyParkingSpot"/> that is not occupied and matches the <see cref="SpotType"/> <paramref name="_value"/>; Otherwise <see langword="null"/></returns>
        /// <exception cref="InvalidCastException"></exception>
        public IMyParkingSpot RequestServiceItem<ValueType> (ValueType _value)
        {
            return FindServiceItem(spot => spot.Type == ParkAndWash.ConvertGeneric<ValueType, SpotType>(_value) && spot.Occupied == false);
        }

        /// <summary>
        /// Verify that an <see cref="IMyParkingSpot"/> <see langword="object"/> that matches the <paramref name="_itemID"/> <see langword="value"/> exists in the collection
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_itemID">The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyParkingSpot"/></param>
        /// <returns><see langword="true"/> if an <see langword="object"/> that matches the <paramref name="_itemID"/> is found; otherwise <see langword="false"/></returns>
        /// <exception cref="InvalidCastException"></exception>
        public bool ValidateServiceItem<IDType> (IDType _itemID)
        {
            if ( FindServiceItem(spot => spot.ID == ParkAndWash.ConvertGeneric<IDType, int>(_itemID)) != null )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Change the unique identifier for <see langword="this"/> <see cref="IMyService{T}"/>
        /// </summary>
        /// <param name="_newID"></param>
        /// <returns><see langword="true"/> if <paramref name="_newID"/> is not already an <see cref="IMyService{T}"/> identifier; Otherwise <see langword="false"/></returns>
        public bool ChangeServiceID (string _newID)
        {
            if ( ParkAndWash.ServiceHandler[_newID] == null )
            {
                ServiceID = _newID;

                return true;
            }

            return false;
        }
    }
}