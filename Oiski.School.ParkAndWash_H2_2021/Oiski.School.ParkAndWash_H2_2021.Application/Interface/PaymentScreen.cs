using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Application.Interface
{
    /// <summary>
    /// Defines a screen with payment methods
    /// </summary>
    public class PaymentScreen : BaseScreen
    {
        /// <summary>
        /// Initialize a new instance of type <see cref="PaymentScreen"/>
        /// </summary>
        public PaymentScreen () : base (100, 30, _enableBackButton: true, "Payment")
        {
            BackButton.OnSelect += ( s ) =>
            {
                SwapScreen (MainScreen.Screen);

                MarkTarget (s, _revert: true);

            };
        }

        private static PaymentScreen screen;
        /// <summary>
        /// The access point for the screen properties
        /// </summary>
        public static PaymentScreen Screen
        {
            get
            {
                if ( screen == null )
                {
                    screen = new PaymentScreen ();
                }

                return screen;
            }
        }

        /// <summary>
        /// Color the text value of <paramref name="_control"/>
        /// </summary>
        /// <param name="_control"></param>
        /// <param name="_color"></param>
        private void ColorValue ( IColorableControl _control, RenderColor _color )
        {
            _control.TextColor = _color;
        }

        protected override void InitControls ()
        {
            BuildClock ();

            #region Ticket ID
            #region Label
            ColorableLabel ticketIDLabel = CreateControl<ColorableLabel> ("Ticket ID");
            ticketIDLabel.Position = new Vector2 (30, Header.Position.y + Header.Size.y + 7);
            #endregion

            #region Value
            ColorableTextField ticketIDValue = CreateControl<ColorableTextField> ("...");
            ticketIDValue.Position = new Vector2 (ticketIDLabel.Position.x + ticketIDLabel.Size.x - 1, ticketIDLabel.Position.y);
            ticketIDValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            ticketIDValue.SelectedIndex = Vector2.Zero;

            ticketIDValue.EraseTextOnSelect = true;
            ticketIDValue.ResetAfterFirstWrite = false;

            ticketIDValue.OnSelect += ( s ) =>
            {
                if ( !OiskiEngine.Input.CanWrite )
                {
                    if ( int.TryParse (s.Text, out int _id) )
                    {
                        IMyTicket ticket = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyTicket>> ("TicketService").FindServiceItem (ticket => ticket.ID == _id);

                        if ( ticket != null )
                        {
                            TicketScreen.Screen.Ticket = ticket;
                            TicketScreen.Screen.Finalize = true;
                            SwapScreen (TicketScreen.Screen);

                            MarkTarget (s, _revert: true);
                            return;
                        }
                    }

                    ColorValue (s as IColorableControl, new RenderColor (ConsoleColor.Red, ConsoleColor.Black));
                }
                else
                {
                    ColorValue (s as IColorableControl, new RenderColor (ConsoleColor.Green, ConsoleColor.Black));
                }
            };
            #endregion

            MenuControl.Controls.AddControl (ticketIDLabel);
            MenuControl.Controls.AddControl (ticketIDValue);
            #endregion
        }
    }
}
