using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.ConsoleTech.Engine.Controls;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;
using Oiski.School.ParkAndWash_H2_2021.Washing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Application.Interface
{
    public class CarWashScreen : BaseScreen
    {
        private CarWashScreen () : base (100, 30, _enableBackButton: true)
        {
            BackButton.OnSelect += ( s ) =>
            {
                SwapScreen (MainScreen.Screen);

                MarkTarget (s, _revert: true);

            };

            BackButton.SelectedIndex = new Vector2 (0, BackButton.SelectedIndex.y - 1);
        }

        /// <summary>
        /// The type of <see cref="IMyCarWash"/> requested
        /// </summary>
        private CarWashType washType = CarWashType.Bronze;

        private static CarWashScreen screen = null;
        /// <summary>
        /// The access point for the screen properties
        /// </summary>
        public static CarWashScreen Screen
        {
            get
            {
                if ( screen == null )
                {
                    screen = new CarWashScreen ();
                }

                return screen;
            }
        }

        protected override void InitControls ()
        {
            BuildClock ();

            #region Wash Overview
            ColorableListBox<int> washOverview = CreateControl<int> ("Wash Overview", 72, 3);
            washOverview.SelectedIndex = new Vector2 (-1, -1);
            washOverview.Position = new Vector2 (Vector2.CenterX (washOverview.Size.x), 5);
            washOverview.SelectableItems = false;
            washOverview.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);

            #region Populate
            washOverview.Items.AddItem (new ListBoxItem<int> ("Bronze: Soaping -> Scrubbing -> Blasting -> Drying", 0));
            washOverview.Items.AddItem (new ListBoxItem<int> ("Silver: Soaping -> Scrubbing -> Rinsing -> Blasting -> Drying", 0));
            washOverview.Items.AddItem (new ListBoxItem<int> ("Gold: Soaping -> Scrubbing -> Rinsing -> Waxing -> Blasting -> Drying", 0));
            #endregion

            MenuControl.Controls.AddControl (washOverview);
            #endregion

            #region Bronze Price
            #region Label
            ColorableLabel bronzePriceLabel = CreateControl<ColorableLabel> ("Bronze Wash");
            bronzePriceLabel.Position = new Vector2 (15, washOverview.Position.y + washOverview.Size.y - 1);
            #endregion

            #region Value
            ColorableLabel bronzePriceValue = CreateControl<ColorableLabel> ("79.95DKK");
            bronzePriceValue.Position = new Vector2 (bronzePriceLabel.Position.x + bronzePriceLabel.Size.x - 1, bronzePriceLabel.Position.y);
            bronzePriceValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (bronzePriceLabel);
            MenuControl.Controls.AddControl (bronzePriceValue);
            #endregion

            #region Silver Price
            #region Label
            ColorableLabel silverPriceLabel = CreateControl<ColorableLabel> ("Silver Wash");
            silverPriceLabel.Position = new Vector2 (bronzePriceValue.Position.x + bronzePriceValue.Size.x + 2, bronzePriceValue.Position.y);
            #endregion

            #region Value
            ColorableLabel silverPriceValue = CreateControl<ColorableLabel> ("169.95DKK");
            silverPriceValue.Position = new Vector2 (silverPriceLabel.Position.x + silverPriceLabel.Size.x - 1, silverPriceLabel.Position.y);
            silverPriceValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (silverPriceLabel);
            MenuControl.Controls.AddControl (silverPriceValue);
            #endregion

            #region Gold Price
            #region Label
            ColorableLabel goldPriceLabel = CreateControl<ColorableLabel> ("Gold Wash");
            goldPriceLabel.Position = new Vector2 (silverPriceValue.Position.x + silverPriceValue.Size.x + 2, silverPriceValue.Position.y);
            #endregion

            #region Value
            ColorableLabel goldPriceValue = CreateControl<ColorableLabel> ("169.95DKK");
            goldPriceValue.Position = new Vector2 (goldPriceLabel.Position.x + goldPriceLabel.Size.x - 1, goldPriceLabel.Position.y);
            goldPriceValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (goldPriceLabel);
            MenuControl.Controls.AddControl (goldPriceValue);
            #endregion

            #region Type
            #region Label
            ColorableLabel washTypeLabel = CreateControl<ColorableLabel> ("Vehicle Type");
            washTypeLabel.Position = new Vector2 (30, bronzePriceLabel.Position.y + bronzePriceLabel.Size.y + 3);
            #endregion

            #region Button
            ColorableOption washTypeButton = CreateControl<ColorableOption> (washType.ToString ());

            #region OnSelect
            washTypeButton.OnSelect += ( s ) =>
            {
                s.Text = ( ( --washType < CarWashType.Gold ) ? ( washType = CarWashType.Bronze ) : ( washType ) ).ToString ();
            };
            washTypeButton.Position = new Vector2 (washTypeLabel.Position.x + washTypeLabel.Size.x - 1, washTypeLabel.Position.y);
            #endregion

            #region OnUpdate
            washTypeButton.OnUpdate += ( c ) => CombiSelect (c, washTypeLabel);
            #endregion
            washTypeButton.TextColor = new ConsoleTech.Engine.Color.Rendering.RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            washTypeButton.SelectedIndex = Vector2.Zero;
            #endregion

            MenuControl.Controls.AddControl (washTypeLabel);
            MenuControl.Controls.AddControl (washTypeButton);
            #endregion

            #region Accept
            ColorableOption acceptButton = CreateControl<ColorableOption> ("Start Wash");
            acceptButton.SelectedIndex = new Vector2 (0, 1);
            acceptButton.Position = new Vector2 (washTypeButton.Position.x + washTypeButton.Size.x + 10, washTypeButton.Position.y);

            #region OnSelect
            acceptButton.OnSelect += ( s ) =>
            {
                IMyCarWash wash = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyCarWash>> ("CarWashService").RequestServiceItem (washType);

                if ( wash != null )
                {
                    IMyTicket ticket = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyTicket>> ("TicketService").RequestServiceItem (KeyValuePair.Create ("wStandard", wash.ID));

                    if ( ticket != null )
                    {
                        decimal price = 0M;

                        switch ( washType )
                        {
                            case CarWashType.Gold:
                                price = 219.95M;
                                break;
                            case CarWashType.Silver:
                                price = 169.95M;
                                break;
                            case CarWashType.Bronze:
                                price = 79.95M;
                                break;
                            default:
                                break;
                        };

                        ticket.SetProperty ("WashPrice", price);
                        ticket.SetProperty ("WashType", washType);
                        if ( !TicketRepository.Link.InsertData (ticket as IMyRepositoryEntity<int, string>) )
                        {
                            TicketRepository.Link.DeleteData (ticket as IMyRepositoryEntity<int, string>);
                            TicketRepository.Link.InsertData (ticket as IMyRepositoryEntity<int, string>);
                        }
                        if ( CarWashRepository.Link.UpdateData (wash as IMyRepositoryEntity<int, string>) )
                        {
                            wash.StartWashAsync ();
                            TicketScreen.Screen.Ticket = ticket;
                            SwapScreen (TicketScreen.Screen);
                            MarkTarget (s, _revert: true);
                        }
                    }
                }
            };
            #endregion

            MenuControl.Controls.AddControl (acceptButton);
            #endregion
        }
    }
}
