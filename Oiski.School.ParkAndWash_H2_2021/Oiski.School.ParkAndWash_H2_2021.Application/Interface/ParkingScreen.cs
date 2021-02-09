using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.ConsoleTech.Engine.Controls;
using Oiski.School.ParkAndWash_H2_2021.Parking;
using Oiski.School.ParkAndWash_H2_2021.Ticketing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Oiski.School.ParkAndWash_H2_2021.Application.Interface
{
    /// <summary>
    /// The parking screen where users can request <see cref="IMyParkingSpot"/> <see langword="objects"/>
    /// </summary>
    public class ParkingScreen : BaseScreen
    {
        /// <summary>
        /// Initialize a new instance of type <see cref="ParkingScreen"/>
        /// </summary>
        public ParkingScreen () : base (100, 30, _enableBackButton: true)
        {
            BackButton.OnSelect += ( s ) =>
            {
                SwapScreen (MainScreen.Screen);

                MarkTarget (s, _revert: true);

            };

            BackButton.SelectedIndex = new Vector2 (0, BackButton.SelectedIndex.y - 1);
        }

        private static ParkingScreen screen = null;
        /// <summary>
        /// The access point for the screen properties
        /// </summary>
        public static ParkingScreen Screen
        {
            get
            {
                if ( screen == null )
                {
                    screen = new ParkingScreen ();
                }

                return screen;
            }
        }

        /// <summary>
        /// The type of <see cref="IMyParkingSpot"/> requested
        /// </summary>
        private SpotType vehicleType = SpotType.Standard;
        /// <summary>
        /// Whether or not the requested <see cref="IMyParkingSpot"/> should have a charging station
        /// </summary>
        private bool chargeable = false;
        /// <summary>
        /// Whether or not the requested <see cref="IMyParkingSpot"/> should come with a car wash
        /// </summary>
        private bool includeCarWash = false;
        /// <summary>
        /// The parking lot layout display
        /// </summary>
        private ColorableListBox<int> parkingLot;
        /// <summary>
        /// The amount of unoccupied standard parking spots left
        /// </summary>
        private ColorableLabel standardAmount;
        /// <summary>
        /// The amount of unoccupied utility parking spots left
        /// </summary>
        private ColorableLabel utilAmount;
        /// <summary>
        /// The amount of unoccupied large parking spots left
        /// </summary>
        private ColorableLabel largeAmount;
        /// <summary>
        /// The amount of unoccupied handicap parking spots left
        /// </summary>
        private ColorableLabel handicapAmount;

        /// <summary>
        /// Will perform <see cref="BaseScreen.MarkTarget(Label, bool)"/> on <paramref name="_control"/> and <paramref name="_label"/>
        /// </summary>
        /// <param name="_control"></param>
        /// <param name="_label"></param>
        private void CombiSelect ( Control _control, Label _label )
        {

            if ( _control is SelectableControl _sControl && _sControl.SelectedIndex == OiskiEngine.Input.GetSelectedIndex )
            {
                MarkTarget (_label);
            }
            else
            {
                MarkTarget (_label, _revert: true);
            }
        }

        /// <summary>
        /// Collects the <see langword="bool"/> value from <paramref name="_state"/> and flips it before the <see langword="outing"/> of <paramref name="_boolean"/>.
        /// </summary>
        /// <param name="_state"></param>
        /// <param name="_boolean"></param>
        /// <returns>The flipped value of <paramref name="_state"/> as a <see langword="string"/> <see langword="value"/></returns>
        private string CollectBoolean ( string _state, out bool _boolean )
        {
            bool.TryParse (_state, out _boolean);
            _boolean = !_boolean;
            return _boolean.ToString ();
        }

        /// <summary>
        /// Update the <see cref="parkingLot"/> <see cref="Control"/>
        /// </summary>
        private void UpdateParkingLotOverview ()
        {
            string[,] spotCells = new string[ 16, 6 ];
            IMyParkingSpot[] spots = ParkingRepository.Link.GetEnumerable ().ToArray ();

            for ( int row = 0, spotIndex = 0; row < spotCells.GetLength (0); row++ )
            {
                #region Build Cell Map

                /*
                    Iterates over each parking spot and maps each occupation value.
                    The Occupation value is mapped first and then the value is mapped into the grid.
                    'X' = Occupied
                    ' ' = Not Occupied

                    Each row is sectioned into different spot maps - See Parking Lot Overview Region -> Populate Reqion -> Parking Lot Layout Region
                    The magic numbers are directly connected to the row number in that parking lot layout
                 */
                for ( int cell = 0; cell < spotCells.GetLength (1); cell++ )
                {
                    if ( row < 7 || row == 11 )
                    {
                        spotCells[ row, cell ] = ( ( spots[ spotIndex ].Occupied ) ? ( "[X]" ) : ( "[ ]" ) );   //  Map Occupation Value
                    }
                    else if ( row > 7 && row < 10 )
                    {
                        if ( cell > 3 ) //  Stay inside the index bounds of the map row
                        {
                            continue;
                        }

                        spotCells[ row, cell ] = ( ( spots[ spotIndex ].Occupied ) ? ( "[X]" ) : ( "[ ]" ) );   //  Map Occupation Value
                    }
                    else if ( row == 7 || row > 11 )
                    {
                        if ( cell > 3 ) //  Stay inside the index bounds of the map row
                        {
                            continue;
                        }

                        spotCells[ row, cell ] = ( ( spots[ spotIndex ].Occupied ) ? ( "[X]" ) : ( "[ ]" ) );   //  Map Occupation Value
                    }
                    else if ( row == 10 )   //  Stay inside the index bounds of the map row
                    {
                        if ( cell > 0 )
                        {
                            continue;
                        }

                        spotCells[ row, cell ] = ( ( spots[ spotIndex ].Occupied ) ? ( "[X]" ) : ( "[ ]" ) );   //  Map Occupation Value
                    }

                    spotIndex++;
                }
                #endregion

                InsertParkingValues (spotCells, row);   //  Map Occupation value into the grid
            }
        }

        /// <summary>
        /// Update the <see cref="Control"/> <see langword="objects"/> that contain the amount of available parking spots
        /// </summary>
        private void UpdateAmountValues ()
        {
            #region Standard Amount
            int availableStandardSpots = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyParkingSpot>> ("ParkingService").FindAllServiceItems (spot => spot.Occupied == false && spot.Type == SpotType.Standard).Count;
            standardAmount.Text = $"{availableStandardSpots:00}/50";
            #endregion

            #region Util Amount
            int availableUtilSpots = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyParkingSpot>> ("ParkingService").FindAllServiceItems (spot => !spot.Occupied && spot.Type == SpotType.Util).Count;
            utilAmount.Text = $"{availableUtilSpots}/10";
            #endregion

            #region Large Amount
            int availableLargeSpots = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyParkingSpot>> ("ParkingService").FindAllServiceItems (spot => !spot.Occupied && spot.Type == SpotType.Large).Count;
            largeAmount.Text = $"{availableLargeSpots}/12";
            #endregion

            #region Handicap Amount
            int availableHandicapSpots = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyParkingSpot>> ("ParkingService").FindAllServiceItems (spot => !spot.Occupied && spot.Type == SpotType.Handicap).Count;
            handicapAmount.Text = $"{availableHandicapSpots}/5";
            #endregion
        }

        /// <summary>
        /// Insert <paramref name="_spots"/> into the <see cref="parkingLot"/> grid
        /// </summary>
        /// <param name="_spots">The range of lot mappings</param>
        /// <param name="_itemIndex">The row number in the <see cref="parkingLot"/> layout</param>
        private void InsertParkingValues ( string[,] _spots, int _itemIndex )
        {

            if ( _itemIndex < 7 || _itemIndex == 11 )
            {
                parkingLot.Items[ _itemIndex ].Text = $"{_spots[ _itemIndex, 0 ]}{_spots[ _itemIndex, 1 ]}{_spots[ _itemIndex, 2 ]} {_spots[ _itemIndex, 3 ]}{_spots[ _itemIndex, 4 ]}{_spots[ _itemIndex, 5 ]}";
            }
            else if ( _itemIndex > 7 && _itemIndex < 10 )
            {
                parkingLot.Items[ _itemIndex ].Text = $"{_spots[ _itemIndex, 0 ]}{_spots[ _itemIndex, 1 ]}       {_spots[ _itemIndex, 2 ]}{_spots[ _itemIndex, 3 ]}";
            }
            else if ( _itemIndex == 7 || _itemIndex > 11 )
            {
                parkingLot.Items[ _itemIndex ].Text = $"   {_spots[ _itemIndex, 0 ]}{_spots[ _itemIndex, 1 ]} {_spots[ _itemIndex, 2 ]}{_spots[ _itemIndex, 3 ]}   ";
            }
            else if ( _itemIndex == 10 )
            {
                parkingLot.Items[ _itemIndex ].Text = $"        {_spots[ _itemIndex, 0 ]}        ";
            }
        }

        protected override void InitControls ()
        {
            BuildClock ();

            #region Spot Amounts
            #region Standard
            #region Label
            ColorableLabel standardAmountLabel = CreateControl<ColorableLabel> ("Standard");
            standardAmountLabel.Position = new Vector2 (5, 7);
            #endregion

            #region Value
            standardAmount = CreateControl<ColorableLabel> ("NaN");
            standardAmount.Position = new Vector2 (standardAmountLabel.Position.x + standardAmountLabel.Size.x - 1, standardAmountLabel.Position.y);
            standardAmount.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (standardAmountLabel);
            MenuControl.Controls.AddControl (standardAmount);
            #endregion

            #region Util
            #region Label
            ColorableLabel utilAmountLabel = CreateControl<ColorableLabel> ("Util");
            utilAmountLabel.Position = new Vector2 (standardAmount.Position.x + standardAmount.Size.x + 3, standardAmountLabel.Position.y);
            #endregion

            #region Value
            utilAmount = CreateControl<ColorableLabel> ("NaN");
            utilAmount.Position = new Vector2 (utilAmountLabel.Position.x + utilAmountLabel.Size.x - 1, utilAmountLabel.Position.y);
            utilAmount.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (utilAmountLabel);
            MenuControl.Controls.AddControl (utilAmount);
            #endregion

            #region Large
            #region Label
            ColorableLabel largeAmountLabel = CreateControl<ColorableLabel> ("Large");
            largeAmountLabel.Position = new Vector2 (utilAmount.Position.x + utilAmount.Size.x + 3, standardAmountLabel.Position.y);
            #endregion

            #region Value
            largeAmount = CreateControl<ColorableLabel> ("NaN");
            largeAmount.Position = new Vector2 (largeAmountLabel.Position.x + largeAmountLabel.Size.x - 1, largeAmountLabel.Position.y);
            largeAmount.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (largeAmountLabel);
            MenuControl.Controls.AddControl (largeAmount);
            #endregion

            #region Handicap
            #region Label
            ColorableLabel handicapAmountLabel = CreateControl<ColorableLabel> ("Handicap");
            handicapAmountLabel.Position = new Vector2 (largeAmount.Position.x + largeAmount.Size.x + 3, standardAmountLabel.Position.y);
            #endregion

            #region Value
            handicapAmount = CreateControl<ColorableLabel> ("NaN");
            handicapAmount.Position = new Vector2 (handicapAmountLabel.Position.x + handicapAmountLabel.Size.x - 1, handicapAmountLabel.Position.y);
            handicapAmount.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (handicapAmountLabel);
            MenuControl.Controls.AddControl (handicapAmount);
            #endregion

            UpdateAmountValues ();
            #endregion

            #region Vehicle Type
            #region Label
            ColorableLabel vehicleTypeLabel = CreateControl<ColorableLabel> ("Vehicle Type");
            vehicleTypeLabel.Position = new Vector2 (20, standardAmount.Position.y + standardAmount.Size.y + 3);
            #endregion

            #region Button
            ColorableOption vehicleTypeButton = CreateControl<ColorableOption> (vehicleType.ToString ());

            #region OnSelect
            vehicleTypeButton.OnSelect += ( s ) =>
            {
                s.Text = ( ( ++vehicleType > SpotType.Handicap ) ? ( vehicleType = SpotType.Standard ) : ( vehicleType ) ).ToString ();
            };
            vehicleTypeButton.Position = new Vector2 (vehicleTypeLabel.Position.x + vehicleTypeLabel.Size.x - 1, vehicleTypeLabel.Position.y);
            #endregion

            #region OnUpdate
            vehicleTypeButton.OnUpdate += ( c ) => CombiSelect (c, vehicleTypeLabel);
            #endregion
            vehicleTypeButton.TextColor = new ConsoleTech.Engine.Color.Rendering.RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            vehicleTypeButton.SelectedIndex = Vector2.Zero;
            #endregion

            MenuControl.Controls.AddControl (vehicleTypeLabel);
            MenuControl.Controls.AddControl (vehicleTypeButton);
            #endregion

            #region Chargeable
            #region Label
            ColorableLabel chargeableLabel = CreateControl<ColorableLabel> ("Chargeable");
            chargeableLabel.Position = new Vector2 (vehicleTypeLabel.Position.x, vehicleTypeLabel.Position.y + 3);
            #endregion

            #region Button
            ColorableOption chargeableButton = CreateControl<ColorableOption> (chargeable.ToString ());

            #region OnSelect
            chargeableButton.OnSelect += ( s ) =>
            {
                s.Text = CollectBoolean (s.Text, out chargeable);

                ColorBooleanValue (s.Text, s as IColorableControl);
            };
            chargeableButton.Position = new Vector2 (chargeableLabel.Position.x + chargeableLabel.Size.x - 1, chargeableLabel.Position.y);
            #endregion

            #region OnUpdate
            chargeableButton.OnUpdate += ( c ) => CombiSelect (c, chargeableLabel);
            #endregion
            chargeableButton.TextColor = new RenderColor (ConsoleColor.Red, ConsoleColor.Black);
            chargeableButton.SelectedIndex = new Vector2 (0, 1);
            #endregion

            MenuControl.Controls.AddControl (chargeableLabel);
            MenuControl.Controls.AddControl (chargeableButton);
            #endregion

            #region Car Wash Included
            #region Label
            ColorableLabel carWashIncludedLabel = CreateControl<ColorableLabel> ("Include Car Wash");
            carWashIncludedLabel.Position = new Vector2 (chargeableLabel.Position.x, chargeableLabel.Position.y + 3);
            #endregion

            #region Button
            ColorableOption carWashIncludedButton = CreateControl<ColorableOption> (includeCarWash.ToString ());

            #region OnSelect
            carWashIncludedButton.OnSelect += ( s ) =>
            {
                s.Text = CollectBoolean (s.Text, out includeCarWash);
                ColorBooleanValue (s.Text, s as IColorableControl);
            };
            carWashIncludedButton.Position = new Vector2 (carWashIncludedLabel.Position.x + carWashIncludedLabel.Size.x - 1, carWashIncludedLabel.Position.y);
            #endregion

            #region OnUpdate
            carWashIncludedButton.OnUpdate += ( c ) => CombiSelect (c, carWashIncludedLabel);
            #endregion
            carWashIncludedButton.TextColor = new RenderColor (ConsoleColor.Red, ConsoleColor.Black);
            carWashIncludedButton.SelectedIndex = new Vector2 (0, 2);
            #endregion

            MenuControl.Controls.AddControl (carWashIncludedLabel);
            MenuControl.Controls.AddControl (carWashIncludedButton);
            #endregion

            #region Accept
            ColorableOption acceptButton = CreateControl<ColorableOption> ("Accept");
            acceptButton.SelectedIndex = new Vector2 (0, 3);
            acceptButton.Position = new Vector2 (30, OiskiEngine.Configuration.Size.y - 5);

            #region OnSelect
            acceptButton.OnSelect += ( s ) =>
            {
                IMyParkingSpot spot = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyParkingSpot>> ("ParkingService").RequestServiceItem (vehicleType);

                if ( spot != null )
                {
                    spot.Occupied = true;

                    IMyTicket ticket = null;

                    if ( chargeable )
                    {
                        ticket = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyTicket>> ("TicketService").RequestServiceItem (KeyValuePair.Create ("PCharge", spot.ID));
                    }
                    else if ( includeCarWash )
                    {
                        ticket = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyTicket>> ("TicketService").RequestServiceItem (KeyValuePair.Create ("PWash", spot.ID));
                    }
                    else
                    {
                        ticket = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyTicket>> ("TicketService").RequestServiceItem (KeyValuePair.Create ("PStandard", spot.ID));
                    }

                    if ( ticket != null )
                    {
                        if ( !TicketRepository.Link.InsertData (ticket as IMyRepositoryEntity<int, string>) )
                        {
                            TicketRepository.Link.DeleteData (ticket as IMyRepositoryEntity<int, string>);
                            TicketRepository.Link.InsertData (ticket as IMyRepositoryEntity<int, string>);
                        }
                        if ( ParkingRepository.Link.UpdateData (spot as IMyRepositoryEntity<int, string>) )
                        {
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

            #region Parking Lot Overview
            parkingLot = CreateControl<int> ("Parking Spots", 19, 16);
            parkingLot.SelectedIndex = new Vector2 (-1, -1);
            parkingLot.Position = new Vector2 (68, Vector2.CenterY (parkingLot.Size.y));
            parkingLot.SelectableItems = false;
            parkingLot.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);

            #region Populate
            #region Parking Lot Layout
            parkingLot.Items.AddItem (new ListBoxItem<int> ("[ ][ ][ ] [ ][ ][ ]", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("[ ][ ][ ] [ ][ ][ ]", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("[ ][ ][ ] [ ][ ][ ]", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("[ ][ ][ ] [ ][ ][ ]", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("[ ][ ][ ] [ ][ ][ ]", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("[ ][ ][ ] [ ][ ][ ]", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("[ ][ ][ ] [ ][ ][ ]", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("   [ ][ ] [ ][ ]   ", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("[ ][ ]       [ ][ ]", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("[ ][ ]       [ ][ ]", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("        [ ]        ", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("[ ][ ][ ] [ ][ ][ ]", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("   [ ][ ] [ ][ ]   ", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("   [ ][ ] [ ][ ]   ", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("   [ ][ ] [ ][ ]   ", 0));
            parkingLot.Items.AddItem (new ListBoxItem<int> ("   [ ][ ] [ ][ ]   ", 0));
            #endregion

            UpdateParkingLotOverview ();
            #endregion

            MenuControl.Controls.AddControl (parkingLot);
            #endregion
        }

        public override void Show ( bool _visible = true )
        {
            if ( _visible )
            {
                UpdateAmountValues ();
                UpdateParkingLotOverview ();
            }

            base.Show (_visible);
        }
    }
}
