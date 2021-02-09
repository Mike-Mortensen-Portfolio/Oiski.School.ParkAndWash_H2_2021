using Oiski.Common.Generics;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;
using System;
using System.Collections.Generic;

namespace Oiski.School.ParkAndWash_H2_2021.Washing
{
    /// <summary>
    /// The service <see langword="object"/> that handles <see cref="IMyCarWash"/> facilities
    /// </summary>
    public class CarWashService : IMyService<IMyCarWash>
    {
        /// <summary>
        /// Creates a new instance of type <see cref="CarWashService"/>
        /// </summary>
        public CarWashService ()
        {
            items = new List<IMyCarWash> ();
        }

        /// <summary>
        /// The unique identifier for the <see cref="IMyService{T}"/> (<see langword="default"/> is "CarWashService")
        /// </summary>
        public string ServiceID { get; private set; } = "CarWashService";

        private readonly List<IMyCarWash> items;
        /// <summary>
        /// The collection of <see cref="IMyCarWash"/> <see langword="objects"/>
        /// </summary>
        public IReadOnlyList<IMyCarWash> Items
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// Add an <see cref="IMyCarWash"/> item to the collection
        /// </summary>
        /// <param name="_item"></param>
        /// <exception cref="ServiceDuplicateException"></exception>
        public void AddServiceItem ( IMyCarWash _item )
        {
            if ( FindServiceItem (item => item.ID == _item.ID) == null )
            {
                items.Add (_item);
            }
            else
            {
                throw new ServiceDuplicateException ($"An item with ID: {_item.ID} already exists in Car Wash Service");
            }
        }

        /// <summary>
        /// Cancel the wash process of a <see cref="IMyCarWash"/>
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_itemID">The <see langword="int"/> ID <see langword="value"/></param>
        /// <returns><see langword="true"/> if the <see cref="IMyCarWash"/> exists and could be canceled; Otherwise <see langword="false"/></returns>
        public bool CancelServiceItem<IDType> ( IDType _itemID )
        {
            IMyCarWash wash = FindServiceItem (item => item.ID == Converter.CastGeneric<IDType, int> (_itemID));

            if ( wash != null )
            {
                wash.CancelWash ();

                return true;
            }

            return false;
        }

        public bool ChangeServiceID ( string _newID )
        {
            if ( ParkAndWash.ServiceHandler[ _newID ] == null )
            {
                ServiceID = _newID;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Find all occurences that matches the <paramref name="_predicate"/>
        /// </summary>
        /// <param name="_predicate"></param>
        /// <returns>A collection of <see cref="IMyCarWash"/> where each item matches the <paramref name="_predicate"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IReadOnlyList<IMyCarWash> FindAllServiceItems ( Predicate<IMyCarWash> _predicate )
        {
            return items.FindAll (_predicate);
        }

        /// <summary>
        /// Find a <see cref="IMyCarWash"/> <see langword="object"/>
        /// </summary>
        /// <param name="_predicate"></param>
        /// <returns>The first occurence that matches the <paramref name="_predicate"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IMyCarWash FindServiceItem ( Predicate<IMyCarWash> _predicate )
        {
            return items.Find (_predicate);
        }

        /// <summary>
        /// Remove and <see cref="IMyCarWash"/> item from the collection
        /// </summary>
        /// <param name="_item"></param>
        /// <returns><see langword="true"/> if the item was found and removed; Otherwise <see langword="false"/></returns>
        public bool RemoveServiceItem ( IMyCarWash _item )
        {
            IMyCarWash wash = items.Find (item => item.ID == _item.ID);
            if ( wash != null )
            {
                items.Remove (wash);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Request a <see cref="IMyCarWash"/> of type <paramref name="_value"/>
        /// </summary>
        /// <typeparam name="ValueType">Must be a <see cref="CarWashType"/> <see langword="value"/></typeparam>
        /// <param name="_value">The <see cref="CarWashType"/> <see langword="value"/> of the requested <see cref="IMyCarWash"/></param>
        /// <returns>An <see cref="IMyCarWash"/> that is not running and matches the <see cref="CarWashType"/> <paramref name="_value"/>; Otherwise <see langword="null"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        public IMyCarWash RequestServiceItem<ValueType> ( ValueType _value )
        {
            IMyCarWash wash = FindServiceItem (item => item.State == CarWashState.NotRunning);
            if ( wash != null )
            {
                switch ( Converter.CastGeneric<ValueType, CarWashType> (_value) )
                {
                    case CarWashType.Gold:
                        wash.Rutine = new CarWashState[] { CarWashState.Soaping, CarWashState.Scrubbing, CarWashState.Blasting, CarWashState.Drying };
                        break;
                    case CarWashType.Silver:
                        wash.Rutine = new CarWashState[] { CarWashState.Soaping, CarWashState.Scrubbing, CarWashState.Rinsing, CarWashState.Blasting, CarWashState.Drying };
                        break;
                    case CarWashType.Bronze:
                        wash.Rutine = new CarWashState[] { CarWashState.Soaping, CarWashState.Scrubbing, CarWashState.Rinsing, CarWashState.Waxing, CarWashState.Blasting, CarWashState.Drying };
                        break;
                    default:
                        throw new ArgumentException ($"Type: {Converter.CastGeneric<ValueType, CarWashType> (_value)} is not valid in this context!");
                }

                return wash;
            }

            return null;
        }

        /// <summary>
        /// Verify that an <see cref="IMyCarWash"/> <see langword="object"/> that matches the <paramref name="_itemID"/> <see langword="value"/> exists in the collection
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_itemID">The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyCarWash"/></param>
        /// <returns><see langword="true"/> if an <see langword="object"/> that matches the <paramref name="_itemID"/> is found; otherwise <see langword="false"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        public bool ValidateServiceItem<IDType> ( IDType _itemID )
        {
            if ( FindServiceItem (item => item.ID == Converter.CastGeneric<IDType, int> (_itemID)) != null )
            {
                return true;
            }

            return false;
        }
    }
}
