﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EleCho.WpfSuite.Controls
{
    /// <inheritdoc/>
    public class Popup : System.Windows.Controls.Primitives.Popup
    {
        private static readonly Action<System.Windows.Controls.Primitives.Popup> s_baseRepositionMethod;
        private static readonly Action<DependencyObject, DependencyPropertyChangedEventArgs> s_baseOnPlacementTargetChangedCallback;

        static Popup()
        {
            var basePopupType = typeof(System.Windows.Controls.Primitives.Popup);

            s_baseRepositionMethod = (Action<System.Windows.Controls.Primitives.Popup>)basePopupType
                .GetMethod("Reposition", BindingFlags.NonPublic | BindingFlags.Instance)!
                .CreateDelegate(typeof(Action<System.Windows.Controls.Primitives.Popup>));

            s_baseOnPlacementTargetChangedCallback = (Action<DependencyObject, DependencyPropertyChangedEventArgs>)basePopupType
                .GetMethod("OnPlacementTargetChanged", BindingFlags.NonPublic | BindingFlags.Static)!
                .CreateDelegate(typeof(Action<DependencyObject, DependencyPropertyChangedEventArgs>));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(Popup), new FrameworkPropertyMetadata(typeof(Popup)));
            PlacementTargetProperty.OverrideMetadata(typeof(Popup), new FrameworkPropertyMetadata(null, PlacementTargetChangedCallback));
        }

        private Window? _placementTargetWindow;

        /// <summary>
        /// Auto reposition if the window position of placement target changed
        /// </summary>
        public bool AutoReposition
        {
            get { return (bool)GetValue(AutoRepositionProperty); }
            set { SetValue(AutoRepositionProperty, value); }
        }

        /// <summary>
        /// WS_EX_NOACTIVATE
        /// </summary>
        public bool NoActivate
        {
            get { return (bool)GetValue(NoActivateProperty); }
            set { SetValue(NoActivateProperty, value); }
        }


        /// <summary>
        /// The DependencyProperty of <see cref="AutoReposition"/> property
        /// </summary>
        public static readonly DependencyProperty AutoRepositionProperty =
            DependencyProperty.Register(nameof(AutoReposition), typeof(bool), typeof(Popup), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// The DependencyProperty of <see cref="NoActivate"/> property
        /// </summary>
        public static readonly DependencyProperty NoActivateProperty =
            DependencyProperty.Register(nameof(NoActivate), typeof(bool), typeof(Popup), new FrameworkPropertyMetadata(true));

        private static void PlacementTargetChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            s_baseOnPlacementTargetChangedCallback.Invoke(d, e);

            if (d is not Popup popup)
            {
                return;
            }

            if (e.OldValue is UIElement oldValue)
            {
                oldValue.LayoutUpdated -= popup.OnPlacementTargetLocationChanged;
            }

            if (e.NewValue is UIElement newValue)
            {
                newValue.LayoutUpdated += popup.OnPlacementTargetLocationChanged;
            }
        }

        /// <inheritdoc/>
        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            if (PlacementTarget is not null &&
                Window.GetWindow(PlacementTarget) is Window placementTargetWindow)
            {
                placementTargetWindow.LocationChanged += OnPlacementTargetLocationChanged;
                _placementTargetWindow = placementTargetWindow;
            }

            if (Child is not null &&
                PresentationSource.FromVisual(Child) is HwndSource hwndSource)
            {
                if (!NoActivate)
                {
                    var exStyle = GetWindowLongPtrW(hwndSource.Handle, GWL_EXSTYLE);
                    var newExStyle = exStyle & ~WS_EX_NOACTIVATE;
                    SetWindowLongPtrW(hwndSource.Handle, GWL_EXSTYLE, newExStyle);
                }
            }
        }

        /// <inheritdoc/>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (_placementTargetWindow is not null)
            {
                _placementTargetWindow.LocationChanged -= OnPlacementTargetLocationChanged;
                _placementTargetWindow = null;
            }
        }

        private void OnPlacementTargetLocationChanged(object? sender, EventArgs e)
        {
            if (AutoReposition)
            {
                s_baseRepositionMethod.Invoke(this);
            }
        }

        const int GWL_EXSTYLE = -20;
        const ulong WS_EX_NOACTIVATE = 0x08000000L;

        [DllImport("User32", EntryPoint = "GetWindowLongPtrW", ExactSpelling = true)]
        static extern ulong GetWindowLongPtrW(nint hwnd, int index);

        [DllImport("User32", EntryPoint = "SetWindowLongPtrW", ExactSpelling = true)]
        static extern nint SetWindowLongPtrW(nint hwnd, int index, ulong newLong);
    }
}
