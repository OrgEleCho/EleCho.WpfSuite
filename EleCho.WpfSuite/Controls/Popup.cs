using System;
using System.Collections.Generic;
using System.Linq;
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

        static Popup()
        {
            var basePopupType = typeof(System.Windows.Controls.Primitives.Popup);

            s_baseRepositionMethod = (Action<System.Windows.Controls.Primitives.Popup>)basePopupType
                .GetMethod("Reposition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                .CreateDelegate(typeof(Action<System.Windows.Controls.Primitives.Popup>));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(Popup), new FrameworkPropertyMetadata(typeof(Popup)));
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
            DependencyProperty.Register(nameof(AutoReposition), typeof(bool), typeof(Popup), new PropertyMetadata(true));



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
