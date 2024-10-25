using System;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// PasswordBox
    /// </summary>
    public class PasswordBox : TextBox
    {
        private readonly StringBuilder _passwordBuilder;
        private readonly StringBuilder _maskTextBuilder;

        private readonly DispatcherTimer _maskTimer;

        static PasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBox), new FrameworkPropertyMetadata(typeof(PasswordBox)));

            InputMethod.IsInputMethodEnabledProperty.OverrideMetadata(typeof(PasswordBox),
                new FrameworkPropertyMetadata(ValueBoxes.FalseBox, FrameworkPropertyMetadataOptions.Inherits, null, new CoerceValueCallback(ForceToFalse)));
        }

        private static object ForceToFalse(DependencyObject d, object baseValue) => ValueBoxes.FalseBox;

        /// <summary>
        /// Initialize a new instance of the LifeStuffPasswordBox class.
        /// </summary>
        public PasswordBox()
        {
            PreviewTextInput += OnPreviewTextInput;
            PreviewKeyDown += OnPreviewKeyDown;

            CommandManager.AddPreviewExecutedHandler(this, PreviewExecutedHandler);

            _passwordBuilder = new();
            _maskTextBuilder = new();

            _maskTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1) };
            _maskTimer.Tick += (sender, args) => MaskAllDisplayText();

        }


        /// <summary>
        /// Password
        /// </summary>
        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        /// <summary>
        /// Mask text
        /// </summary>
        public char Mask
        {
            get { return (char)GetValue(MaskProperty); }
            set { SetValue(MaskProperty, value); }
        }

        /// <summary>
        /// Mask text immediately after input
        /// </summary>
        public bool AlwaysMask
        {
            get { return (bool)GetValue(AlwaysMaskProperty); }
            set { SetValue(AlwaysMaskProperty, value); }
        }





        /// <summary>
        /// The DependencyProperty of <see cref="Password"/> property
        /// </summary>
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(PasswordBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordChanged));

        /// <summary>
        /// The DependencyProperty of <see cref="Mask"/> property
        /// </summary>
        public static readonly DependencyProperty MaskProperty =
            DependencyProperty.Register(nameof(Mask), typeof(char), typeof(PasswordBox), new FrameworkPropertyMetadata('*'));

        /// <summary>
        /// The DependencyProperty of <see cref="AlwaysMask"/> property
        /// </summary>
        public static readonly DependencyProperty AlwaysMaskProperty =
            DependencyProperty.Register(nameof(AlwaysMask), typeof(bool), typeof(PasswordBox), new FrameworkPropertyMetadata(true));

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not PasswordBox pwdBox)
            {
                return;
            }

            pwdBox.SetPassword(pwdBox.Password);
        }


        /// <summary>
        /// Method to handle PreviewExecutedHandler events
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="executedRoutedEventArgs">Event Text Arguments</param>
        private static void PreviewExecutedHandler(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Command == ApplicationCommands.Copy ||
                executedRoutedEventArgs.Command == ApplicationCommands.Cut ||
                executedRoutedEventArgs.Command == ApplicationCommands.Paste)
            {
                executedRoutedEventArgs.Handled = true;
            }
        }

        /// <summary>
        /// Method to handle PreviewTextInput events
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="textCompositionEventArgs">Event Text Arguments</param>
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs textCompositionEventArgs)
        {
            if (CaretIndex > _passwordBuilder.Length)
            {
                // error 
                Text = Text.Substring(0, _passwordBuilder.Length);
                CaretIndex = Text.Length;
            }

            AddToSecureString(textCompositionEventArgs.Text);

            if (AlwaysMask)
            {
                MaskAllDisplayText();
            }

            textCompositionEventArgs.Handled = true;
        }

        /// <summary>
        /// Method to handle PreviewKeyDown events
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="keyEventArgs">Event Text Arguments</param>
        private void OnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            Key pressedKey = keyEventArgs.Key == Key.System ? keyEventArgs.SystemKey : keyEventArgs.Key;
            switch (pressedKey)
            {
                case Key.Space:
                {
                    AddToSecureString(" ");

                    if (AlwaysMask)
                    {
                        MaskAllDisplayText();
                    }

                    keyEventArgs.Handled = true;
                    break;
                }
                case Key.Back:
                case Key.Delete:
                    if (SelectionLength > 0)
                    {
                        RemoveFromSecureString(SelectionStart, SelectionLength);
                    }
                    else if (pressedKey == Key.Delete && CaretIndex < Text.Length)
                    {
                        RemoveFromSecureString(CaretIndex, 1);
                    }
                    else if (pressedKey == Key.Back && CaretIndex > 0)
                    {
                        int caretIndex = CaretIndex;
                        if (CaretIndex > 0 && CaretIndex < Text.Length)
                            caretIndex = caretIndex - 1;
                        RemoveFromSecureString(CaretIndex - 1, 1);
                        CaretIndex = caretIndex;
                    }

                    keyEventArgs.Handled = true;
                    break;
            }
        }

        /// <summary>
        /// Method to add new text into SecureString and process visual output
        /// </summary>
        /// <param name="text">Text to be added</param>
        private void AddToSecureString(string text)
        {
            if (SelectionLength > 0)
            {
                RemoveFromSecureString(SelectionStart, SelectionLength);
            }

            foreach (char c in text)
            {
                int caretIndex = CaretIndex;
                _passwordBuilder.Insert(caretIndex, c);
                MaskAllDisplayText();

                if (caretIndex == Text.Length)
                {
                    _maskTimer.Stop();
                    _maskTimer.Start();

                    Text = Text.Insert(caretIndex++, c.ToString());
                }
                else
                {
                    Text = Text.Insert(caretIndex++, Mask.ToString());
                }

                CaretIndex = caretIndex;
            }

            SetValue(PasswordProperty, _passwordBuilder.ToString());
        }

        /// <summary>
        /// Method to remove text from SecureString and process visual output
        /// </summary>
        /// <param name="startIndex">Start Position for Remove</param>
        /// <param name="trimLength">Length of Text to be removed</param>
        private void RemoveFromSecureString(int startIndex, int trimLength)
        {
            int caretIndex = CaretIndex;
            _passwordBuilder.Remove(startIndex, trimLength);

            Text = Text.Remove(startIndex, trimLength);
            CaretIndex = caretIndex;
            SetValue(PasswordProperty, _passwordBuilder.ToString());
        }

        private void MaskAllDisplayText()
        {
            _maskTimer.Stop();
            int caretIndex = CaretIndex;

            _maskTextBuilder.Clear();
            _maskTextBuilder.Append(Mask, Text.Length);

            Text = _maskTextBuilder.ToString();
            CaretIndex = caretIndex;
        }

        private void SetPassword(string password)
        {
            _maskTimer.Stop();
            int caretIndex = CaretIndex;

            _maskTextBuilder.Clear();
            _maskTextBuilder.Append(Mask, password.Length);

            _passwordBuilder.Clear();
            _passwordBuilder.Append(password);

            Text = _maskTextBuilder.ToString();
            Password = password;
            CaretIndex = caretIndex;
        }
    }
}
