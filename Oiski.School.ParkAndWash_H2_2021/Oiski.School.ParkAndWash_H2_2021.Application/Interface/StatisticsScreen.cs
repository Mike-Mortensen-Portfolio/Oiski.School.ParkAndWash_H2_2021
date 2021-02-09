using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.School.ParkAndWash_H2_2021.Washing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.School.ParkAndWash_H2_2021.Application.Interface
{
    public class StatisticsScreen : BaseScreen
    {
        private StatisticsScreen () : base (100, 30, _enableBackButton: true, "Statistics")
        {
            BackButton.OnSelect += ( s ) =>
            {
                SwapScreen (MainScreen.Screen);

                MarkTarget (s, _revert: true);

            };
        }

        #region Value Fields
        private ColorableLabel facility0NameValue;
        private ColorableLabel facility0RunsValue;
        private ColorableLabel facility0ProgressValue;
        private ColorableLabel facility0StateValue;
        private ColorableOption facility0Button;

        private ColorableLabel facility1NameValue;
        private ColorableLabel facility1RunsValue;
        private ColorableLabel facility1ProgressValue;
        private ColorableLabel facility1StateValue;
        private ColorableOption facility1Button;

        private ColorableLabel facility2NameValue;
        private ColorableLabel facility2RunsValue;
        private ColorableLabel facility2ProgressValue;
        private ColorableLabel facility2StateValue;
        private ColorableOption facility2Button;
        #endregion

        private static StatisticsScreen screen;
        public static StatisticsScreen Screen
        {
            get
            {
                if ( screen == null )
                {
                    screen = new StatisticsScreen ();
                }

                return screen;
            }
        }

        public IMyCarWash Facility0 { get; private set; }
        public IMyCarWash Facility1 { get; private set; }
        public IMyCarWash Facility2 { get; private set; }

        /// <summary>
        /// Update the value fields
        /// </summary>
        private void UpdateValues ()
        {
            if ( Facility0 == null || Facility1 == null || Facility2 == null )  //  Ensure that no facility is null
            {
                Facility0 = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyCarWash>> ("CarWashService").FindServiceItem (wash => wash.ID == 1);
                Facility1 = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyCarWash>> ("CarWashService").FindServiceItem (wash => wash.ID == 2);
                Facility2 = ParkAndWash.ServiceHandler.GetServiceAs<IMyService<IMyCarWash>> ("CarWashService").FindServiceItem (wash => wash.ID == 3);
            }

            #region Facility 0
            facility0NameValue.Text = Facility0.Name;
            facility0RunsValue.Position = new Vector2 (facility0NameValue.Position.x + facility0NameValue.Size.x - 1, facility0NameValue.Position.y);
            facility0RunsValue.Text = $"{Facility0.TimesRun:000}";
            facility0ProgressValue.Text = $"{Facility0.ProcessProgress:000.00}%";
            facility0StateValue.Text = Facility0.State.ToString ();
            #endregion

            #region Facility 1
            facility1NameValue.Text = Facility1.Name;
            facility1RunsValue.Position = new Vector2 (facility1NameValue.Position.x + facility1NameValue.Size.x - 1, facility1NameValue.Position.y);
            facility1RunsValue.Text = $"{Facility1.TimesRun:000}";
            facility1ProgressValue.Text = $"{Facility1.ProcessProgress:000.00}%";
            facility1StateValue.Text = Facility1.State.ToString ();
            #endregion

            #region Facility 2
            facility2NameValue.Text = Facility2.Name;
            facility2RunsValue.Position = new Vector2 (facility2NameValue.Position.x + facility2NameValue.Size.x - 1, facility2NameValue.Position.y);
            facility2RunsValue.Text = $"{Facility2.TimesRun:000}";
            facility2ProgressValue.Text = $"{Facility2.ProcessProgress:000.00}%";
            facility2StateValue.Text = Facility2.State.ToString ();
            #endregion
        }

        protected override void InitControls ()
        {
            BuildClock ();

            #region Facility 0
            #region Name + Runs
            #region Label
            facility0NameValue = CreateControl<ColorableLabel> ("Name");
            facility0NameValue.Position = new Vector2 (11, Header.Position.y + Header.Size.y + 2);
            #endregion

            #region Value
            facility0RunsValue = CreateControl<ColorableLabel> ($"{0:000}");
            facility0RunsValue.Position = new Vector2 (facility0NameValue.Position.x + facility0NameValue.Size.x - 1, facility0NameValue.Position.y);
            facility0RunsValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (facility0NameValue);
            MenuControl.Controls.AddControl (facility0RunsValue);
            #endregion

            #region Progress
            #region Label
            ColorableLabel facility0ProgressLabel = CreateControl<ColorableLabel> ("Progress");
            facility0ProgressLabel.Position = new Vector2 (facility0NameValue.Position.x, facility0NameValue.Position.y + facility0NameValue.Size.y - 1);
            #endregion

            #region Value
            facility0ProgressValue = CreateControl<ColorableLabel> ($"{0:000.00}%");
            facility0ProgressValue.Position = new Vector2 (facility0ProgressLabel.Position.x + facility0ProgressLabel.Size.x - 1, facility0ProgressLabel.Position.y);
            facility0ProgressValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);

            facility0ProgressValue.OnUpdate += ( c ) =>
            {
                if ( Facility0 != null )
                {
                    ( ( ColorableLabel ) c ).Text = $"{Facility0.ProcessProgress:000.00}%";
                }
            };
            #endregion

            MenuControl.Controls.AddControl (facility0ProgressLabel);
            MenuControl.Controls.AddControl (facility0ProgressValue);
            #endregion

            #region State
            #region Label
            ColorableLabel facility0StateLabel = CreateControl<ColorableLabel> ("State");
            facility0StateLabel.Position = new Vector2 (facility0ProgressLabel.Position.x, facility0ProgressLabel.Position.y + facility0ProgressLabel.Size.y - 1);
            #endregion

            #region Value
            facility0StateValue = CreateControl<ColorableLabel> ($"{CarWashState.NotRunning}");
            facility0StateValue.Position = new Vector2 (facility0StateLabel.Position.x + facility0StateLabel.Size.x - 1, facility0StateLabel.Position.y);
            facility0StateValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);

            facility0StateValue.OnUpdate += ( c ) =>
            {
                if ( Facility0 != null )
                {
                    ( ( ColorableLabel ) c ).Text = $"{Facility0.State}";
                }
            };
            #endregion

            MenuControl.Controls.AddControl (facility0StateLabel);
            MenuControl.Controls.AddControl (facility0StateValue);
            #endregion

            #region Accept
            facility0Button = CreateControl<ColorableOption> ("Cancel");
            facility0Button.SelectedIndex = new Vector2 (0, 0);
            facility0Button.Position = new Vector2 (facility0StateLabel.Position.x + 3, facility0StateLabel.Position.y + 3);

            #region OnSelect
            facility0Button.OnSelect += ( s ) =>
            {
                if ( Facility0 != null )
                {
                    Facility0.AbortWash ();
                }
            };
            #endregion

            MenuControl.Controls.AddControl (facility0Button);
            #endregion
            #endregion

            #region Facility 1
            #region Name + Runs
            #region Label
            facility1NameValue = CreateControl<ColorableLabel> ("Name");
            facility1NameValue.Position = new Vector2 (facility0NameValue.Position.x + facility0NameValue.Size.x + facility0RunsValue.Position.x + facility0RunsValue.Size.x + 3, Header.Position.y + Header.Size.y + 2);
            #endregion

            #region Value
            facility1RunsValue = CreateControl<ColorableLabel> ($"{0:000}");
            facility1RunsValue.Position = new Vector2 (facility1NameValue.Position.x + facility1NameValue.Size.x - 1, facility1NameValue.Position.y);
            facility1RunsValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (facility1NameValue);
            MenuControl.Controls.AddControl (facility1RunsValue);
            #endregion

            #region Progress
            #region Label
            ColorableLabel facility1ProgressLabel = CreateControl<ColorableLabel> ("Progress");
            facility1ProgressLabel.Position = new Vector2 (facility1NameValue.Position.x, facility1NameValue.Position.y + facility1NameValue.Size.y - 1);
            #endregion

            #region Value
            facility1ProgressValue = CreateControl<ColorableLabel> ($"{0:000.00}%");
            facility1ProgressValue.Position = new Vector2 (facility1ProgressLabel.Position.x + facility1ProgressLabel.Size.x - 1, facility1ProgressLabel.Position.y);
            facility1ProgressValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);

            facility1ProgressValue.OnUpdate += ( c ) =>
            {
                if ( Facility1 != null )
                {
                    ( ( ColorableLabel ) c ).Text = $"{Facility1.ProcessProgress:000.00}%";
                }
            };
            #endregion

            MenuControl.Controls.AddControl (facility1ProgressLabel);
            MenuControl.Controls.AddControl (facility1ProgressValue);
            #endregion

            #region State
            #region Label
            ColorableLabel facility1StateLabel = CreateControl<ColorableLabel> ("State");
            facility1StateLabel.Position = new Vector2 (facility1ProgressLabel.Position.x, facility1ProgressLabel.Position.y + facility1ProgressLabel.Size.y - 1);
            #endregion

            #region Value
            facility1StateValue = CreateControl<ColorableLabel> ($"{CarWashState.NotRunning}");
            facility1StateValue.Position = new Vector2 (facility1StateLabel.Position.x + facility1StateLabel.Size.x - 1, facility1StateLabel.Position.y);
            facility1StateValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);

            facility1StateValue.OnUpdate += ( c ) =>
            {
                if ( Facility1 != null )
                {
                    ( ( ColorableLabel ) c ).Text = $"{Facility1.State}";
                }
            };
            #endregion

            MenuControl.Controls.AddControl (facility1StateLabel);
            MenuControl.Controls.AddControl (facility1StateValue);
            #endregion

            #region Accept
            facility1Button = CreateControl<ColorableOption> ("Cancel");
            facility1Button.SelectedIndex = new Vector2 (0, 1);
            facility1Button.Position = new Vector2 (facility1StateLabel.Position.x + 3, facility1StateLabel.Position.y + 3);

            #region OnSelect
            facility1Button.OnSelect += ( s ) =>
            {
                if ( Facility1 != null )
                {
                    Facility1.AbortWash ();
                }
            };
            #endregion

            MenuControl.Controls.AddControl (facility1Button);
            #endregion
            #endregion

            #region Facility 2
            #region Name + Runs
            #region Label
            facility2NameValue = CreateControl<ColorableLabel> ("Name");
            facility2NameValue.Position = new Vector2 (facility0NameValue.Position.x + facility1NameValue.Size.x + facility1RunsValue.Position.x + facility1RunsValue.Size.x + 3, Header.Position.y + Header.Size.y + 2);
            #endregion

            #region Value
            facility2RunsValue = CreateControl<ColorableLabel> ($"{0:000}");
            facility2RunsValue.Position = new Vector2 (facility2NameValue.Position.x + facility2NameValue.Size.x - 1, facility2NameValue.Position.y);
            facility2RunsValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            #endregion

            MenuControl.Controls.AddControl (facility2NameValue);
            MenuControl.Controls.AddControl (facility2RunsValue);
            #endregion

            #region Progress
            #region Label
            ColorableLabel facility2ProgressLabel = CreateControl<ColorableLabel> ("Progress");
            facility2ProgressLabel.Position = new Vector2 (facility2NameValue.Position.x, facility2NameValue.Position.y + facility2NameValue.Size.y - 1);
            #endregion

            #region Value
            facility2ProgressValue = CreateControl<ColorableLabel> ($"{0:000.00}%");
            facility2ProgressValue.Position = new Vector2 (facility2ProgressLabel.Position.x + facility2ProgressLabel.Size.x - 1, facility2ProgressLabel.Position.y);
            facility2ProgressValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);

            facility2ProgressValue.OnUpdate += ( c ) =>
            {
                if ( Facility2 != null )
                {
                    ( ( ColorableLabel ) c ).Text = $"{Facility2.ProcessProgress:000.00}%";
                }
            };
            #endregion

            MenuControl.Controls.AddControl (facility2ProgressLabel);
            MenuControl.Controls.AddControl (facility2ProgressValue);
            #endregion

            #region State
            #region Label
            ColorableLabel facility2StateLabel = CreateControl<ColorableLabel> ("State");
            facility2StateLabel.Position = new Vector2 (facility2ProgressLabel.Position.x, facility2ProgressLabel.Position.y + facility2ProgressLabel.Size.y - 1);
            #endregion

            #region Value
            facility2StateValue = CreateControl<ColorableLabel> ($"{CarWashState.NotRunning}");
            facility2StateValue.Position = new Vector2 (facility2StateLabel.Position.x + facility2StateLabel.Size.x - 1, facility2StateLabel.Position.y);
            facility2StateValue.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);

            facility2StateValue.OnUpdate += ( c ) =>
            {
                if ( Facility2 != null )
                {
                    ( ( ColorableLabel ) c ).Text = $"{Facility2.State}";
                }
            };
            #endregion

            MenuControl.Controls.AddControl (facility2StateLabel);
            MenuControl.Controls.AddControl (facility2StateValue);
            #endregion

            #region Accept
            facility2Button = CreateControl<ColorableOption> ("Cancel");
            facility2Button.SelectedIndex = new Vector2 (0, 2);
            facility2Button.Position = new Vector2 (facility2StateLabel.Position.x + 2, facility2StateLabel.Position.y + 3);

            #region OnSelect
            facility2Button.OnSelect += ( s ) =>
            {
                if ( Facility2 != null )
                {
                    Facility2.AbortWash ();
                }
            };
            #endregion

            MenuControl.Controls.AddControl (facility2Button);
            #endregion
            #endregion
        }

        public override void Show ( bool _visible = true )
        {
            if ( _visible )
            {
                UpdateValues ();
            }

            base.Show (_visible);
        }
    }
}
