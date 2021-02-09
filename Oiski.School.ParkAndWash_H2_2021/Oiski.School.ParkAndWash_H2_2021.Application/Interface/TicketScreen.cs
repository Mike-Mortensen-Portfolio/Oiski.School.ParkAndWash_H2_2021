using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.School.ParkAndWash_H2_2021.Parking;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Application.Interface
{
    /// <summary>
    /// A screen that can display <see cref="IMyTicket"/> information
    /// </summary>
    public class TicketScreen : BaseScreen
    {
        /// <summary>
        /// Initialize a new istance of type <see cref="TicketScreen"/>
        /// </summary>
        private TicketScreen () : base (50, 46, _enableBackButton: true, "Ticket")
        {
            BackButton.OnSelect += ( s ) =>
             {
                 if ( Finalize )
                 {
                     ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyTicket>> ("TicketService").RemoveServiceItem (Ticket);
                     TicketRepository.Link.DeleteData (Ticket as IMyRepositoryEntity<int, string>);
                 }

                 SwapScreen (MainScreen.Screen);
                 MarkTarget (s, _revert: true);
             };
        }

        #region Label Fields
        private ColorableLabel ticketIDLabel;
        private ColorableLabel ticketIDValue;

        private ColorableLabel ticketTypeLabel;
        private ColorableLabel ticketTypeValue;

        private ColorableLabel ticketTimeStampLabel;
        private ColorableLabel ticketTimeStampValue;

        private ColorableLabel attachedEntityTypeLabel;
        private ColorableLabel attachedEntityTypeValue;

        private ColorableLabel chargeableStationLabel;
        private ColorableLabel chargeableStationValue;

        private ColorableLabel washIncludedLabel;
        private ColorableLabel washIncludedValue;

        private ColorableLabel serviceIncludedLabel;
        private ColorableLabel serviceIncludedValue;

        private ColorableLabel priceLabel;
        private ColorableLabel priceValue;

        private ColorableLabel spotFeeLabel;
        private ColorableLabel spotFeeValue;

        private ColorableLabel totalPriceLabel;
        private ColorableLabel totalPriceValue;
        #endregion

        private static TicketScreen screen;
        /// <summary>
        /// The access point for the screen properties
        /// </summary>
        public static TicketScreen Screen
        {
            get
            {
                if ( screen == null )
                {
                    screen = new TicketScreen ();
                }

                return screen;
            }
        }

        /// <summary>
        /// The <see cref="IMyTicket"/> to display
        /// </summary>
        public IMyTicket Ticket { get; set; }

        /// <summary>
        /// If <see langword="true"/> the <see cref="Ticket"/> is considered the final product and a <i>final cost</i> will be calculated; Otherwise, if <see langword="false"/>, the <i>total cost</i> is not included in the screen view
        /// </summary>
        public bool Finalize { get; set; }

        /// <summary>
        /// Update the <see cref="ColorableLabel"/> <see langword="objects"/> fields with data from <see cref="Ticket"/>
        /// </summary>
        private void UpdateValues ()
        {
            IMyParkingTicket pTicket = Ticket as IMyParkingTicket;
            IMyCarWashTicket wTicket = Ticket as IMyCarWashTicket;

            #region Chargeable Station Value
            if ( pTicket != null )
            {
                chargeableStationLabel.Render = true;
                chargeableStationValue.Render = true;

                chargeableStationValue.Text = ( pTicket.TicketType.Name == "ParkingChargeTicket" ).ToString ();
                ColorBooleanValue (chargeableStationValue.Text, chargeableStationValue);
            }
            else
            {
                chargeableStationLabel.Render = false;
                chargeableStationValue.Render = false;
            }
            #endregion

            #region Wash Included Value
            if ( pTicket != null )
            {
                washIncludedLabel.Render = true;
                washIncludedValue.Render = true;

                washIncludedValue.Text = ( pTicket.TicketType.Name == "ParkingWashTicket" ).ToString ();
                ColorBooleanValue (washIncludedValue.Text, washIncludedValue);
            }
            else
            {
                washIncludedLabel.Render = false;
                washIncludedValue.Render = false;
            }
            #endregion

            #region Total Cost
            if ( Finalize )
            {
                totalPriceLabel.Render = true;
                totalPriceValue.Render = true;

                if ( pTicket != null )
                {
                    spotFeeLabel.Render = true;
                    spotFeeValue.Render = true;

                    TimeSpan occupationTime = DateTime.Now - pTicket.OccupationStamp;

                    if ( occupationTime.TotalHours >= 48 )
                    {
                        IMyParkingTicket oldTicket = Ticket as IMyParkingTicket;
                        IMyTicket newTicket = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyTicket>> ("TicketService").RequestServiceItem (KeyValuePair.Create ("PService", oldTicket.ParkingSpotID));
                        newTicket.SetProperty ("OccupationStamp", oldTicket.OccupationStamp);
                        newTicket.SetProperty ("OccupationPricePrHour", oldTicket.OccupationPricePrHour);
                        newTicket.SetProperty ("ServiceType", "Basic Service Check");

                        ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyTicket>> ("TicketService").CancelServiceItem (Ticket.ID);
                        Ticket = newTicket;
                        pTicket = newTicket as IMyParkingTicket;
                    }

                    decimal hourPrice = pTicket.OccupationPricePrHour * ( decimal ) occupationTime.TotalHours;
                    decimal minutePrice = ( pTicket.OccupationPricePrHour / 60 ) * ( decimal ) occupationTime.TotalMinutes;
                    decimal totalHourlyPrice = hourPrice + minutePrice;

                    decimal spotFee = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyParkingSpot>> ("ParkingService").FindServiceItem (spot => spot.ID == pTicket.ParkingSpotID).SpotFee;
                    decimal totalCost = totalHourlyPrice + spotFee;

                    totalPriceValue.Text = $"{totalCost:00.00}DKK";
                }
                else if ( wTicket != null )
                {
                    totalPriceValue.Text = $"{wTicket.WashPrice:00.00}DKK";
                }
            }
            else
            {
                totalPriceLabel.Render = false;
                totalPriceValue.Render = false;
            }

            #endregion

            #region Ticket ID Value
            ticketIDValue.Text = Ticket.ID.ToString ();
            #endregion

            #region Ticket Type Value
            ticketTypeValue.Text = Ticket.TicketType.Name;
            #endregion

            #region Ticket Time Stamp Value
            if ( Ticket is IMyParkingTicket _timeParkingTicket )
            {
                ticketTimeStampLabel.Render = true;
                ticketTimeStampValue.Render = true;
                ticketTimeStampValue.Text = _timeParkingTicket.OccupationStamp.ToString ();
            }
            else
            {
                ticketTimeStampLabel.Render = false;
                ticketTimeStampValue.Render = false;
            }
            #endregion

            #region Attached Entity Type Value
            if ( pTicket != null )
            {
                attachedEntityTypeLabel.Text = "Parking Spot Type";
                UpdateLabelPositionX (attachedEntityTypeValue, attachedEntityTypeLabel);
                attachedEntityTypeValue.Text = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyParkingSpot>> ("ParkingService").FindServiceItem (spot => spot.ID == pTicket.ParkingSpotID).Type.ToString ();
            }
            else if ( wTicket != null )
            {
                attachedEntityTypeLabel.Text = "Car Wash Type";
                UpdateLabelPositionX (attachedEntityTypeValue, attachedEntityTypeLabel);
                attachedEntityTypeValue.Text = wTicket.WashType.ToString ();
            }
            #endregion

            #region Service Included Value
            if ( pTicket != null )
            {
                serviceIncludedLabel.Render = true;
                serviceIncludedValue.Render = true;

                serviceIncludedValue.Text = ( pTicket.TicketType.Name == "ParkingServiceTicket" ).ToString ();
                ColorBooleanValue (serviceIncludedValue.Text, serviceIncludedValue);
            }
            else
            {
                serviceIncludedLabel.Render = false;
                serviceIncludedValue.Render = false;
            }
            #endregion

            #region Price Pr. Hour Value
            if ( pTicket != null )
            {
                priceLabel.Text = "Price Pr. Hour";
                UpdateLabelPositionX (priceValue, priceLabel);
                priceValue.Text = $"{pTicket.OccupationPricePrHour:00.00}DKK";
            }
            else if ( wTicket != null )
            {
                priceLabel.Text = "Wash Price";
                UpdateLabelPositionX (priceValue, priceLabel);
                priceValue.Text = $"{wTicket.WashPrice:00.00}DKK";
            }
            #endregion

            #region Spot Fee Value
            if ( pTicket != null )
            {
                spotFeeLabel.Render = true;
                spotFeeValue.Render = true;

                spotFeeValue.Text = $"{ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyParkingSpot>> ("ParkingService").FindServiceItem (spot => spot.ID == pTicket.ParkingSpotID).SpotFee:00.00}DKK";
            }
            else
            {
                spotFeeLabel.Render = false;
                spotFeeValue.Render = false;
            }
            #endregion
        }

        /// <summary>
        /// Update the position of <paramref name="_label"/> relative to <paramref name="_basedOn"/>
        /// </summary>
        /// <param name="_label">The label to position</param>
        /// <param name="_basedOn">The label to base the positoning on</param>
        private void UpdateLabelPositionX ( ColorableLabel _label, ColorableLabel _basedOn )
        {
            _label.Position = new Vector2 (_basedOn.Position.x + _basedOn.Size.x - 1, _basedOn.Position.y);
        }

        protected override void InitControls ()
        {
            #region Ticket ID
            #region Label
            ticketIDLabel = CreateControl<ColorableLabel> ("Ticket ID");
            ticketIDLabel.Position = new Vector2 (5, Header.Position.y + Header.Size.y + 2);
            #endregion

            #region Value
            ticketIDValue = CreateControl<ColorableLabel> ("NaN");
            ticketIDValue.Position = new Vector2 (ticketIDLabel.Position.x + ticketIDLabel.Size.x - 1, ticketIDLabel.Position.y);
            ticketIDValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (ticketIDLabel);
            MenuControl.Controls.AddControl (ticketIDValue);
            #endregion

            #region Ticket Type
            #region Label
            ticketTypeLabel = CreateControl<ColorableLabel> ("Ticket Type");
            ticketTypeLabel.Position = new Vector2 (5, ticketIDLabel.Position.y + ticketIDLabel.Size.y);
            #endregion

            #region Value
            ticketTypeValue = CreateControl<ColorableLabel> ("NaN");
            ticketTypeValue.Position = new Vector2 (ticketTypeLabel.Position.x + ticketTypeLabel.Size.x - 1, ticketTypeLabel.Position.y);
            ticketTypeValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (ticketTypeLabel);
            MenuControl.Controls.AddControl (ticketTypeValue);
            #endregion

            #region Ticket Time Stamp
            #region Label
            ticketTimeStampLabel = CreateControl<ColorableLabel> ("Time Stamp");
            ticketTimeStampLabel.Position = new Vector2 (5, ticketTypeLabel.Position.y + ticketTypeLabel.Size.y);
            #endregion

            #region Value
            ticketTimeStampValue = CreateControl<ColorableLabel> ("NaN");
            ticketTimeStampValue.Position = new Vector2 (ticketTimeStampLabel.Position.x + ticketTimeStampLabel.Size.x - 1, ticketTimeStampLabel.Position.y);
            ticketTimeStampValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (ticketTimeStampLabel);
            MenuControl.Controls.AddControl (ticketTimeStampValue);
            #endregion

            #region Attached Entity Type
            #region Label
            attachedEntityTypeLabel = CreateControl<ColorableLabel> ("Entity Type");
            attachedEntityTypeLabel.Position = new Vector2 (5, ticketTimeStampLabel.Position.y + ticketTimeStampLabel.Size.y);
            #endregion

            #region Value
            attachedEntityTypeValue = CreateControl<ColorableLabel> ("NaN");
            attachedEntityTypeValue.Position = new Vector2 (attachedEntityTypeLabel.Position.x + attachedEntityTypeLabel.Size.x - 1, attachedEntityTypeLabel.Position.y);
            attachedEntityTypeValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (attachedEntityTypeLabel);
            MenuControl.Controls.AddControl (attachedEntityTypeValue);
            #endregion

            #region Chargeable Station
            #region Label
            chargeableStationLabel = CreateControl<ColorableLabel> ("Charge Station");
            chargeableStationLabel.Position = new Vector2 (5, attachedEntityTypeLabel.Position.y + attachedEntityTypeLabel.Size.y);
            #endregion

            #region Value
            chargeableStationValue = CreateControl<ColorableLabel> ("NaN");
            chargeableStationValue.Position = new Vector2 (chargeableStationLabel.Position.x + chargeableStationLabel.Size.x - 1, chargeableStationLabel.Position.y);
            chargeableStationValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (chargeableStationLabel);
            MenuControl.Controls.AddControl (chargeableStationValue);
            #endregion

            #region Wash Included
            #region Label
            washIncludedLabel = CreateControl<ColorableLabel> ("Wash Included");
            washIncludedLabel.Position = new Vector2 (5, chargeableStationLabel.Position.y + chargeableStationLabel.Size.y);
            #endregion

            #region Value
            washIncludedValue = CreateControl<ColorableLabel> ("NaN");
            washIncludedValue.Position = new Vector2 (washIncludedLabel.Position.x + washIncludedLabel.Size.x - 1, washIncludedLabel.Position.y);
            washIncludedValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (washIncludedLabel);
            MenuControl.Controls.AddControl (washIncludedValue);
            #endregion

            #region Service Included
            #region Label
            serviceIncludedLabel = CreateControl<ColorableLabel> ("Service Included");
            serviceIncludedLabel.Position = new Vector2 (5, washIncludedLabel.Position.y + washIncludedLabel.Size.y);
            #endregion

            #region Value
            serviceIncludedValue = CreateControl<ColorableLabel> ("NaN");
            serviceIncludedValue.Position = new Vector2 (serviceIncludedLabel.Position.x + serviceIncludedLabel.Size.x - 1, serviceIncludedLabel.Position.y);
            serviceIncludedValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (serviceIncludedLabel);
            MenuControl.Controls.AddControl (serviceIncludedValue);
            #endregion

            #region Price
            #region Label
            priceLabel = CreateControl<ColorableLabel> ("Price Pr. Hour");
            priceLabel.Position = new Vector2 (5, serviceIncludedLabel.Position.y + serviceIncludedLabel.Size.y);
            #endregion

            #region Value
            priceValue = CreateControl<ColorableLabel> ("NaN");
            priceValue.Position = new Vector2 (priceLabel.Position.x + priceLabel.Size.x - 1, priceLabel.Position.y);
            priceValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (priceLabel);
            MenuControl.Controls.AddControl (priceValue);
            #endregion

            #region SpotFee
            #region Label
            spotFeeLabel = CreateControl<ColorableLabel> ("Parking Spot Fee");
            spotFeeLabel.Position = new Vector2 (5, priceLabel.Position.y + priceLabel.Size.y);
            #endregion

            #region Value
            spotFeeValue = CreateControl<ColorableLabel> ("NaN");
            spotFeeValue.Position = new Vector2 (spotFeeLabel.Position.x + spotFeeLabel.Size.x - 1, spotFeeLabel.Position.y);
            spotFeeValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (spotFeeLabel);
            MenuControl.Controls.AddControl (spotFeeValue);
            #endregion

            #region Total Cost
            #region Label
            totalPriceLabel = CreateControl<ColorableLabel> ("Total Cost");
            totalPriceLabel.Position = new Vector2 (5, spotFeeLabel.Position.y + spotFeeLabel.Size.y);
            #endregion

            #region Value
            totalPriceValue = CreateControl<ColorableLabel> ("NaN");
            totalPriceValue.Position = new Vector2 (totalPriceLabel.Position.x + totalPriceLabel.Size.x - 1, totalPriceLabel.Position.y);
            totalPriceValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (totalPriceLabel);
            MenuControl.Controls.AddControl (totalPriceValue);
            #endregion
        }

        public override void Show ( bool _visible = true )
        {
            if ( _visible )
            {
                BackButton.Text = ( ( !Finalize ) ? ( "Back" ) : ( "Pay" ) );

                UpdateValues ();
            }

            base.Show (_visible);
        }
    }
}
