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
            items = new List<IMyTicket> ();
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
        public void AddServiceItem ( IMyTicket _item )
        {
            if ( !ValidateServiceItem (_item.ID) )
            {
                items.Add (_item);
            }
            else
            {
                throw new ServiceDuplicateException ($"An item with ID: {_item.ID} already exists in Ticket Service");
            }
        }

        /// <summary>
        /// Cancel the reservation of an <see cref="IMyTicket"/>. Effectively destroying it.
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_itemID">The <see langword="int"/> ID <see langword="value"/></param>
        /// <returns><see langword="true"/> if the <see cref="IMyTicket"/> exists and could be canceled; Otherwise <see langword="false"/></returns>
        /// <exception cref="InvalidCastException"></exception>
        public bool CancelServiceItem<IDType> ( IDType _itemID )
        {
            IMyTicket ticket = FindServiceItem (ticket => ticket.ID == Common.Generics.Converter.CastGeneric<IDType, int> (_itemID));

            return RemoveServiceItem (ticket);
        }

        /// <summary>
        /// Find a <see cref="IMyTicket"/> <see langword="object"/>
        /// </summary>
        /// <param name="_predicate"></param>
        /// <returns>The first occurence that matches the <paramref name="_predicate"/></returns>
        public IMyTicket FindServiceItem ( Predicate<IMyTicket> _predicate )
        {
            return items.Find (_predicate);
        }

        /// <summary>
        /// Find all occurences that matches the <paramref name="_predicate"/>
        /// </summary>
        /// <param name="_predicate"></param>
        /// <returns>A collection of <see cref="IMyTicket"/> where each item matches the <paramref name="_predicate"/></returns>
        public IReadOnlyList<IMyTicket> FindAllServiceItems ( Predicate<IMyTicket> _predicate )
        {
            return items.FindAll (_predicate);
        }

        /// <summary>
        /// Remove an <see cref="IMyTicket"/> item from the collection
        /// </summary>
        /// <param name="_item"></param>
        /// <returns><see langword="true"/> if the item was found and removed; Otherwise <see langword="false"/></returns>
        public bool RemoveServiceItem ( IMyTicket _item )
        {
            if ( ValidateServiceItem (_item.ID) )
            {
                IMyTicket ticket = FindServiceItem (ticket => ticket.ID == _item.ID);

                return items.Remove (ticket);
            }

            return false;
        }

        /// <summary>
        /// Generate an <see cref="IMyTicket"/> based on the passed in <paramref name="_value"/>
        /// 
        /// <list type="table">
        ///     <listheader>
        ///         <term>Key</term>
        ///         <term>Type</term>
        ///         <term>Description</term>
        ///     </listheader>
        ///     <item>
        ///         <description>| PStandard |</description>
        ///         <description><see cref="IMyParkingTicket"/> |</description>
        ///         <description>A standard parking ticket |</description>
        ///     </item>
        ///     <item>
        ///         <description>| PCharge |</description>
        ///         <description><see cref="IMyParkingTicket"/> |</description>
        ///         <description>A parking ticket that gives access to an <see cref="Parking.IMyParkingSpot"/> with a charge station |</description>
        ///     </item>
        ///     <item>
        ///         <description>| PService |</description>
        ///         <description><see cref="IMyParkingTicket"/> |</description>
        ///         <description>A standard parking ticket that includedes a service check |</description>
        ///     </item>
        ///     <item>
        ///         <description>| PWash |</description>
        ///         <description><see cref="IMyParkingTicket"/> |</description>
        ///         <description>A standard parking ticket that includes a car wash |</description>
        ///     </item>
        ///     <item>
        ///         <description>| WStandard |</description>
        ///         <description><see cref="IMyCarWashTicket"/> |</description>
        ///         <description>A standard car wash ticket |</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <typeparam name="ValueType">Must be an <see cref="KeyValuePair{TKey, TValue}"/> where the <i>key</i> <see langword="value"/> is the <see langword="string"/> 'Type Key' and <i>value</i> is the <see langword="int"/> ID <see langword="value"/> for the attached <see langword="object"/> the ticket represents</typeparam>
        /// <param name="_value">The <see cref="KeyValuePair{TKey, TValue}"/> that contains the <see langword="string"/> <i>key</i> <see langword="value"/> that defines the requested <see cref="IMyTicket"/>, and the <see langword="int"/> <i>ID</i> <see langword="value"/> that represents the <see langword="object"/> attached to that <see cref="IMyTicket"/></param>
        /// <returns>An <see cref="IMyTicket"/> <see langword="object"/>, if the requested ticket could be created; Otherwise <see langword="null"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="OverflowException"></exception>
        public IMyTicket RequestServiceItem<ValueType> ( ValueType _value )
        {
            IMyTicket ticket = null;
            KeyValuePair<string, int> ticketInfo = Common.Generics.Converter.CastGeneric<ValueType, KeyValuePair<string, int>> (_value);
            switch ( ticketInfo.Key.ToLower () )
            {
                case "pstandard":
                    ticket = Factory.CreateParkingTicket (ParkingTicketType.Standard);
                    ticket.SetProperty ("ParkingSpotID", ticketInfo.Value);
                    ticket.SetProperty ("OccupationPricePrHour", 10M);
                    break;
                case "pcharge":
                    ticket = Factory.CreateParkingTicket (ParkingTicketType.ParkingCharge);
                    ticket.SetProperty ("ParkingSpotID", ticketInfo.Value);
                    ticket.SetProperty ("OccupationPricePrHour", 15M);
                    break;
                case "pservice":
                    ticket = Factory.CreateParkingTicket (ParkingTicketType.ParkingService);
                    ticket.SetProperty ("ParkingSpotID", ticketInfo.Value);
                    ticket.SetProperty ("OccupationPricePrHour", 10M);
                    break;
                case "pwash":
                    ticket = Factory.CreateParkingTicket (ParkingTicketType.ParkingWash);
                    ticket.SetProperty ("ParkingSpotID", ticketInfo.Value);
                    ticket.SetProperty ("OccupationPricePrHour", 20M);
                    break;
                case "wstandard":
                    ticket = Factory.CreateDefaultCarWashTicket ();
                    ticket.SetProperty ("CarWashID", ticketInfo.Value);
                    break;
                default:
                    break;
            }

            AddServiceItem (ticket);

            return ticket;
        }

        /// <summary>
        /// Verify that an <see cref="IMyTicket"/> <see langword="object"/> that matches the <paramref name="_itemID"/> <see langword="value"/> exists in the collection
        /// </summary>
        /// <typeparam name="IDType">Must be an <see langword="int"/> <see langword="value"/></typeparam>
        /// <param name="_itemID">The <see langword="int"/> <see langword="value"/> that identifies the <see cref="IMyTicket"/></param>
        /// <returns><see langword="true"/> if an <see langword="object"/> that matches the <paramref name="_itemID"/> is found; otherwise <see langword="false"/></returns>
        /// <exception cref="InvalidCastException"></exception>
        public bool ValidateServiceItem<IDType> ( IDType _itemID )
        {
            return FindServiceItem (ticket => ticket.ID == Common.Generics.Converter.CastGeneric<IDType, int> (_itemID)) != null;
        }

        /// <summary>
        /// Change the unique identifier for <see langword="this"/> <see cref="IMyService{T}"/>
        /// </summary>
        /// <param name="_newID"></param>
        /// <returns><see langword="true"/> if <paramref name="_newID"/> is not already an <see cref="IMyService{T}"/> identifier; Otherwise <see langword="false"/></returns>
        public bool ChangeServiceID ( string _newID )
        {
            if ( ParkAndWash.ServiceHandler[ _newID ] != null )
            {
                ServiceID = _newID;
                return true;
            }

            return false;
        }
    }
}