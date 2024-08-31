using System.Windows;

namespace EleCho.WpfSuite.Controls
{
    public class ContextMenu : System.Windows.Controls.ContextMenu
    {
        static ContextMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ContextMenu), new FrameworkPropertyMetadata(typeof(ContextMenu)));
        }


        /// <summary>
        /// The CornerRadius property allows users to control the roundness of the corners independently by
        /// setting a radius value for each corner.  Radius values that are too large are scaled so that they
        /// smoothly blend from corner to corner.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(ContextMenu));
    }
}
