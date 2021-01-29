using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Ticketing
{
    /// <summary>
    /// The service <see langword="object"/> that handles <see cref="IMyTicket"/>s
    /// </summary>
    internal class TicketService : IMyService<IMyTicket>
    {
        /// <summary>
        /// Creates a new instance of type <see cref="TicketService"/>
        /// </summary>
        public TicketService ()
        {
            items = new List<IMyTicket>();
        }

        /// <summary>
        /// The unique identifier for the <see cref="IMyService{T}"/> (<see langword="default"/> is "TicketService")
        /// </summary>
        public string ServiceID { get; private set; } = "TicketService";

        private readonly List<IMyTicket> items;
        /// <summary>
        /// The collection of <see cref="IMyTicket"/> <see langword="objects"/>
        /// </summary>
        public IReadOnlyList<IMyTicket> Items
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// Add an <see cref="IMyTicket"/> item to the collection
        /// </summary>
        /// <param name="_item"></param>
        /// <exception cref="ServiceDuplicateException"></exception>
        public void AddServiceItem (IMyTicket _item)
        {
            if ( ValidateServiceItem(_item.ID) )
            {
                items.Add(_item);
            }

            throw new ServiceDuplicateException($"An item with ID: {_item.ID} already exists in Ticket Service");
        }

        /// <summary>
        /// Cancel the reservation of an <see cref="IMyTicket"/>. Effectively destroying it.
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_itemID">The <see langword="int"/> ID <see langword="value"/></param>
        /// <returns><see langword="true"/> if the <see cref="IMyTicket"/> exists and could be canceled; Otherwise <see langword="false"/></returns>
        public bool CancelServiceItem<IDType> (IDType _itemID)
        {
            IMyTicket ticket = FindServiceItem(ticket => ticket.ID == ParkAndWash.ConvertGeneric<IDType, int>(_itemID));

            return RemoveServiceItem(ticket);
        }

        /// <summary>
        /// Find a <see cref="IMyTicket"/> <see langword="object"/>
        /// </summary>
        /// <param name="_predicate"></param>
        /// <returns>The first occurence that matches the <paramref name="_predicate"/></returns>
        public IMyTicket FindServiceItem (Predicate<IMyTicket> _predicate)
        {
            return items.Find(_predicate);
        }

        /// <summary>
        /// Find all occurences that matches the <paramref name="_predicate"/>
        /// </summary>
        /// <param name="_predicate"></param>
        /// <returns>A collection of <see cref="IMyTicket"/> where each item matches the <paramref name="_predicate"/></returns>
        public IReadOnlyList<IMyTicket> FindAllServiceItems (Predicate<IMyTicket> _predicate)
        {
            return items.FindAll(_predicate);
        }

        /// <summary>
        /// Remove an <see cref="IMyTicket"/> item from the collection
        /// </summary>
        /// <param name="_item"></param>
        /// <returns><see langword="true"/> if the item was found and removed; Otherwise <see langword="false"/></returns>
        public bool RemoveServiceItem (IMyTicket _item)
        {
            if ( ValidateServiceItem(_item.ID) )
            {
                IMyTicket ticket = FindServiceItem(ticket => ticket.ID == _item.ID);

                return items.Remove(ticket);
            }

            return false;
        }

        /// <summary>
        /// Generate an <see cref="IMyTicket"/> for an occupation of the <see cref="Parking.IMyParkingSpot"/> attached to the <see langword="int"/> ID <paramref name="_value"/>
        /// </summary>
        /// <typeparam name="ValueType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_value">The <see langword="int"/> ID <see langword="value"/> of the requested <see cref="Parking.IMyParkingSpot"/></param>
        /// <returns>An <see cref="IMyTicket"/> that reserves the <see cref="Parking.IMyParkingSpot"/> attached to the ID <paramref name="_value"/>, if the requested ticket could be created; Otherwise <see langword="null"/></returns>
        /// <exception cref="ServiceDuplicateException"></exception>
        public IMyTicket RequestServiceItem<ValueType> (ValueType _value)
        {
            IMyTicket ticket = Factory.CreateParkingTicket(ParkAndWash.ConvertGeneric<ValueType, int>(_value), 150);

            AddServiceItem(ticket);

            return ticket;
        }

        /// <summary>
        /// Verify that an <see cref="IMyTicket"/> <see langword="object"/> that matches the <paramref name="_itemID"/> <see langword="value"/> exists in the collection
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_itemID">The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyTicket"/></param>
        /// <returns></returns>
        public bool ValidateServiceItem<IDType> (IDType _itemID)
        {
            return FindServiceItem(ticket => ticket.ID == ParkAndWash.ConvertGeneric<IDType, int>(_itemID)) != null;
        }

        /// <summary>
        /// Change the unique identifier for <see langword="this"/> <see cref="IMyService{T}"/>
        /// </summary>
        /// <param name="_newID"></param>
        /// <returns><see langword="true"/> if <paramref name="_newID"/> is not already an <see cref="IMyService{T}"/> identifier; Otherwise <see langword="false"/></returns>
        public bool ChangeServiceID (string _newID)
        {
            if ( ParkAndWash.ServiceHandler[_newID] != null )
            {
                ServiceID = _newID;
                return true;
            }

            return false;
        }
    }
}