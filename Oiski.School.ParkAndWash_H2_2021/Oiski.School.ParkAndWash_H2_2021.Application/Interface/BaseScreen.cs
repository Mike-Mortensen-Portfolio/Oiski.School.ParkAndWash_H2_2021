using System;
using Oiski.ConsoleTech.Engine.Controls;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.ConsoleTech.Engine;
using System.Linq;
using System.Runtime.InteropServices;

namespace Oiski.School.ParkAndWash_H2_2021.Application.Interface
{
    /// <summary>
    /// Defines a <see langword="base"/> type for screen displayed in the <see cref="Console"/> Window
    /// </summary>
    public abstract class BaseScreen
    {
        /// <summary>
        /// Initialize a new instance of type <see cref="BaseScreen"/>
        /// </summary>
        /// <param name="_width">The width of the <see cref="Console"/> window</param>
        /// <param name="_height">The height of the <see cref="Console"/> window</param>
        /// <param name="_enableBackButton">Whether or not to render the screens back button</param>
        /// <param name="_headerText">The text of the header displayed on the screen. (<i><strong>Note: </strong> Leave as <see langword="null"/> to disable the header</i>)</param>
        protected BaseScreen ( int _width, int _height, bool _enableBackButton, string _headerText = null )
        {
            MenuControl = new Menu ();

            #region Setting Selction Configuration for MenuControl
            MenuControl.OnTarget = ( s ) =>
            {
                foreach ( var c in MenuControl.Controls.GetSelectableControls )
                {
                    if ( c.SelectedIndex != OiskiEngine.Input.GetSelectedIndex )    //  Reverting the color of each control that is not currently selected back to the default color scheme
                    {
                        MarkTarget (c, _revert: true);
                    }
                }

                MarkTarget (s);
            };
            #endregion

            DefaultTextColor = new RenderColor (ConsoleColor.Blue, ConsoleColor.Black);
            DefaultBorderColor = new RenderColor (ConsoleColor.DarkCyan, ConsoleColor.Black);
            Width = _width;
            Height = _height;
            SetScreenSize (_width, _height);

            if ( _headerText != null )
            {
                BuildHeader (_headerText);
            }

            InitControls ();

            if ( _enableBackButton )
            {
                BuildBackButton ();
            }
        }

        /// <summary>
        /// Whether or not the back button is enabled
        /// </summary>
        protected virtual bool BackButtonEnabled { get; }
        protected virtual int Height { get; }
        protected virtual int Width { get; }
        /// <summary>
        /// The control that handles the controls displayed on the screen
        /// </summary>
        protected Menu MenuControl { get; }
        protected ColorableLabel Header { get; private set; }
        protected ColorableOption BackButton { get; private set; }

        public RenderColor DefaultTextColor { get; set; }
        public RenderColor DefaultBorderColor { get; set; }

        /// <summary>
        /// Builds the header <see cref="Control"/>
        /// </summary>
        /// <param name="_headerText"></param>
        private void BuildHeader ( string _headerText )
        {
            Header = CreateControl<ColorableLabel> (_headerText);
            Header.Position = new Vector2 (Vector2.CenterX (Header.Size.x), 4);
            Header.TextColor = new RenderColor (ConsoleColor.Cyan, ConsoleColor.Black);
            Header.BorderColor = new RenderColor (ConsoleColor.DarkBlue, ConsoleColor.Black);
            MenuControl.Controls.AddControl (Header);
        }

        /// <summary>
        /// Builds the bakc button <see cref="Control"/>
        /// </summary>
        private void BuildBackButton ()
        {
            BackButton = CreateControl<ColorableOption> ("Go Back");
            BackButton.SelectedIndex = new Vector2 (0, MenuControl.Controls.GetSelectableControls.Count);
            BackButton.Position = new Vector2 (Vector2.CenterX (BackButton.Size.x), OiskiEngine.Configuration.Size.y - 5);
            MenuControl.Controls.AddControl (BackButton);
        }

        /// <summary>
        /// Display the screen in the <see cref="Console"/> window
        /// </summary>
        /// <param name="_visible">If <see langword="true"/> the screen will be rendered; Otherwise, if <see langword="false"/>,the screen will be hidden/></param>
        public virtual void Show ( bool _visible = true )
        {
            if ( _visible )
            {
                SetScreenSize (Width, Height);
                OiskiEngine.Input.AtTarget += MenuControl.OnTarget;
            }
            else
            {
                OiskiEngine.Input.AtTarget -= MenuControl.OnTarget;
            }


            MarkTarget (MenuControl.Controls.GetSelectableControls.FirstOrDefault (control => control.SelectedIndex == Vector2.Zero));

            MenuControl.Show (_visible);
        }

        /// <summary>
        /// Initiate the controls that will be rendered on the screen
        /// </summary>
        protected abstract void InitControls ();

        /// <summary>
        /// Switch between two screen (<i><strong>Note: </strong> This will call the <see cref="BaseScreen.Show(bool)"/> with <see langword="false"/> on this screen and call <see cref="BaseScreen.Show(bool)"/> with <see langword="true"/> on <paramref name="_swapTo"/></i>).
        /// <br/>
        /// Effectively hiding this screen and displaying <paramref name="_swapTo"/>
        /// </summary>
        /// <param name="_swapTo">The <see cref="BaseScreen"/> to switch to</param>
        protected virtual void SwapScreen ( BaseScreen _swapTo )
        {
            this.Show (false);
            OiskiEngine.Input.ResetInput ();
            OiskiEngine.Input.ResetSlection ();
            _swapTo.Show (true);
        }

        /// <summary>
        /// This alters the border style and color of <paramref name="_control"/>. (<i><strong>Note: </strong> If <paramref name="_revert"/> is <see langword="true"/>, <paramref name="_control"/> will be reset to it's <see langword="default""/> border style and color</i>)
        /// </summary>
        /// <param name="_control"></param>
        /// <param name="_revert"></param>
        protected virtual void MarkTarget ( Label _control, bool _revert = false )
        {
            if ( _revert )
            {
                IColorableControl cC = _control as IColorableControl;
                cC.BorderColor = DefaultBorderColor;
                _control.BorderStyle (BorderArea.Horizontal, '-');
            }
            else
            {
                IColorableControl control = _control as IColorableControl;
                control.BorderColor = new RenderColor (ConsoleColor.DarkBlue, ConsoleColor.Black);
                _control.BorderStyle (BorderArea.Horizontal, '~');
            }
        }

        /// <summary>
        /// Adjust the screen size
        /// </summary>
        /// <param name="_width">The width of the screen</param>
        /// <param name="_height">The height of the screen</param>
        protected void SetScreenSize ( int _width, int _height )
        {
            Console.SetWindowSize (_width, _height);
            Console.SetBufferSize (_width, _height + 1);

            OiskiEngine.Configuration.Size = new Vector2 (_width, _height);
        }

        /// <summary>
        /// Color the text of a <see cref="IColorableControl"/> based on <paramref name="_boolean"/>. (<i><strong>Note: </strong>If <see langword="true"/> the text will be colored green; Otherwise, if false, colored red</i>)
        /// </summary>
        /// <param name="_boolean"></param>
        /// <param name="_control"></param>
        protected virtual void ColorBooleanValue ( string _boolean, IColorableControl _control )
        {
            if ( _boolean.ToLower () == "false" )
            {
                _control.TextColor = new RenderColor (ConsoleColor.Red, ConsoleColor.Black);
            }
            else
            {
                _control.TextColor = new RenderColor (ConsoleColor.Green, ConsoleColor.Black);
            }
        }

        /// <summary>
        /// Will perform <see cref="BaseScreen.MarkTarget(Label, bool)"/> on <paramref name="_control"/> and <paramref name="_label"/>
        /// </summary>
        /// <param name="_control"></param>
        /// <param name="_label"></param>
        protected void CombiSelect ( Control _control, Label _label )
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
        /// Create a new instance of type <typeparamref name="T"/> where the text of the <see cref="Control"/> is set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_text"></param>
        /// <returns>The created <typeparamref name="T"/> <see langword="object"/></returns>
        protected T CreateControl<T> ( string _text ) where T : Control, IColorableControl
        {
            T control = null;

            if ( typeof (T) == typeof (ColorableLabel) )
            {
                control = new ColorableLabel (_text, DefaultTextColor, DefaultBorderColor, false) as T;
            }
            else if ( typeof (T) == typeof (ColorableOption) )
            {
                control = new ColorableOption (_text, DefaultTextColor, DefaultBorderColor, false) as T;
            }
            else if ( typeof (T) == typeof (ColorableTextField) )
            {
                control = new ColorableTextField (_text, DefaultTextColor, DefaultBorderColor, false) as T;
            }

            return control;
        }
        /// <summary>
        /// Create a new instance of type <see cref="ColorableListBox{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_headerText"></param>
        /// <param name="_width"></param>
        /// <param name="_height"></param>
        /// <returns>The creates <see cref="ColorableListBox{T}"/></returns>
        protected ColorableListBox<T> CreateControl<T> ( string _headerText, int _width, int _height )
        {
            return new ColorableListBox<T> (_headerText, _height, _width, DefaultTextColor, DefaultBorderColor, false);
        }

        /// <summary>
        /// Build the clock <see cref="Control"/>
        /// </summary>
        protected void BuildClock ()
        {
            ColorableLabel time = CreateControl<ColorableLabel> (DateTime.Now.ToString ());
            time.OnUpdate += ( c ) =>
            {
                time.Text = DateTime.Now.ToString ();
            };
            time.Position = new Vector2 (5, 0);
            time.SetBorder (BorderArea.Corner, false);
            time.SetBorder (BorderArea.Horizontal, false);

            MenuControl.Controls.AddControl (time);
        }
    }
}
