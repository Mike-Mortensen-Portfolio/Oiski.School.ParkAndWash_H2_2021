using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.School.ParkAndWash_H2_2021.Washing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Application.Interface
{
    /// <summary>
    /// The main screen for the application
    /// </summary>
    public class MainScreen : BaseScreen
    {
        /// <summary>
        /// Initialize a new instance of type <see cref="MainScreen"/>
        /// </summary>
        private MainScreen () : base (100, 30, false, "Welcome to Park'N Wash")
        {

        }

        private static MainScreen screen = null;
        /// <summary>
        /// The access point for the screen properties
        /// </summary>
        public static MainScreen Screen
        {
            get
            {
                if ( screen == null )
                {
                    screen = new MainScreen ();
                }

                return screen;
            }
        }

        protected override void InitControls ()
        {
            BuildClock ();

            #region Park Button
            ColorableOption toParkSection = CreateControl<ColorableOption> ("Request Parking Spot");
            toParkSection.SelectedIndex = Vector2.Zero;
            toParkSection.Position = new Vector2 (Vector2.CenterX (toParkSection.Size.x), Header.Position.y + Header.Size.y + 3);
            toParkSection.BorderStyle (ConsoleTech.Engine.Controls.BorderArea.Horizontal, '~');
            toParkSection.BorderColor = new RenderColor (ConsoleColor.DarkBlue, ConsoleColor.Black);

            toParkSection.OnSelect += ( s ) =>
            {
                SwapScreen (ParkingScreen.Screen);
                MarkTarget (s, _revert: true);
            };

            MenuControl.Controls.AddControl (toParkSection);
            #endregion

            #region Wash Button
            ColorableOption toWashingSection = CreateControl<ColorableOption> ("Request Car Wash");
            toWashingSection.SelectedIndex = new Vector2 (0, 1);
            toWashingSection.Position = new Vector2 (Vector2.CenterX (toWashingSection.Size.x), toParkSection.Position.y + toParkSection.Size.y);

            toWashingSection.OnSelect += ( s ) =>
            {
                SwapScreen (CarWashScreen.Screen);
                MarkTarget (s, _revert: true);
            };

            MenuControl.Controls.AddControl (toWashingSection);
            #endregion 

            #region Statistics Button
            ColorableOption toStatisticsSection = CreateControl<ColorableOption> ("Statistics");
            toStatisticsSection.SelectedIndex = new Vector2 (0, 2);
            toStatisticsSection.Position = new Vector2 (Vector2.CenterX (toStatisticsSection.Size.x), toWashingSection.Position.y + toWashingSection.Size.y);

            toStatisticsSection.OnSelect += ( s ) =>
            {
                SwapScreen (StatisticsScreen.Screen);
                MarkTarget (s, _revert: true);
            };

            MenuControl.Controls.AddControl (toStatisticsSection);
            #endregion 

            #region Payment Button
            ColorableOption toPaymentSection = CreateControl<ColorableOption> ("Payments");
            toPaymentSection.SelectedIndex = new Vector2 (0, 3);
            toPaymentSection.Position = new Vector2 (Vector2.CenterX (toPaymentSection.Size.x), toStatisticsSection.Position.y + toStatisticsSection.Size.y);

            toPaymentSection.OnSelect += ( s ) =>
            {
                SwapScreen (PaymentScreen.Screen);
                MarkTarget (s, _revert: true);
            };

            MenuControl.Controls.AddControl (toPaymentSection);
            #endregion 
        }
    }
}
