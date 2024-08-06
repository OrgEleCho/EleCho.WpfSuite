using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EleCho.WpfSuite
{
    /// <inheritdoc/>
    public class Popup : System.Windows.Controls.Primitives.Popup
    {
        private static readonly Action<System.Windows.Controls.Primitives.Popup> s_baseRepositionMethod;

        private static readonly Action<DependencyObject, DependencyPropertyChangedEventArgs> s_baseOnPlacementTargetChangedMethod;

        static Popup()
        {
            var basePopupType = typeof(System.Windows.Controls.Primitives.Popup);

            s_baseRepositionMethod = (Action<System.Windows.Controls.Primitives.Popup>)basePopupType
                .GetMethod("Reposition", BindingFlags.NonPublic | BindingFlags.Instance)!
                .CreateDelegate(typeof(Action<System.Windows.Controls.Primitives.Popup>));

            s_baseOnPlacementTargetChangedMethod = (Action<DependencyObject, DependencyPropertyChangedEventArgs>)basePopupType
                .GetMethod("OnPlacementTargetChanged", BindingFlags.NonPublic | BindingFlags.Static)!
                .CreateDelegate(typeof(Action<DependencyObject, DependencyPropertyChangedEventArgs>));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(Popup), new FrameworkPropertyMetadata(typeof(Popup)));
            PlacementTargetProperty.OverrideMetadata(typeof(Popup), new FrameworkPropertyMetadata(null, (dp, v) =>
            {
                s_baseOnPlacementTargetChangedMethod.Invoke(dp, v);
                if (dp is Popup popup)
                {
                    if (popup.PlacementTarget is not null)
                    {
                        popup.PlacementTarget.LayoutUpdated -= popup.OnPlacementTargetWindowLocationChanged;
                    }
                    if (popup.AutoReposition && v.NewValue is not null)
                    {
                        ((UIElement)v.NewValue).LayoutUpdated += popup.OnPlacementTargetWindowLocationChanged;
                    }
                }

            }));
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
        /// The DependencyProperty of <see cref="AutoReposition"/> property
        /// </summary>
        public static readonly DependencyProperty AutoRepositionProperty =
            DependencyProperty.Register(nameof(AutoReposition), typeof(bool), typeof(Popup), new PropertyMetadata(true, AutoRepositionCallback));

        private static void AutoRepositionCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Popup popup)
            {
                return;
            }

            if (e.NewValue is true)
            {
                if (popup._placementTargetWindow is not null)
                {
                    popup._placementTargetWindow.LocationChanged += popup.OnPlacementTargetWindowLocationChanged;
                }
                if (popup.PlacementTarget is not null)
                {
                    popup.PlacementTarget.LayoutUpdated += popup.OnPlacementTargetWindowLocationChanged;
                }
            }
            else
            {
                if (popup._placementTargetWindow is not null)
                {
                    popup._placementTargetWindow.LocationChanged -= popup.OnPlacementTargetWindowLocationChanged;
                }
                if (popup.PlacementTarget is not null)
                {
                    popup.PlacementTarget.LayoutUpdated -= popup.OnPlacementTargetWindowLocationChanged;
                }
            }
        }



        /// <inheritdoc/>
        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            if (AutoReposition &&
                PlacementTarget is not null &&
                Window.GetWindow(PlacementTarget) is Window placementTargetWindow)
            {
                placementTargetWindow.LocationChanged += OnPlacementTargetWindowLocationChanged;
                _placementTargetWindow = placementTargetWindow;
            }

        }

        /// <inheritdoc/>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (_placementTargetWindow is not null)
            {
                _placementTargetWindow.LocationChanged -= OnPlacementTargetWindowLocationChanged;
                _placementTargetWindow = null;
            }
        }

        private void OnPlacementTargetWindowLocationChanged(object? sender, EventArgs e)
        {
            s_baseRepositionMethod.Invoke(this);
        }
    }
}
