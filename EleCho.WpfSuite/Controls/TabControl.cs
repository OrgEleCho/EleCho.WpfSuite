using System.Windows;

namespace EleCho.WpfSuite
{
    public class TabControl : System.Windows.Controls.TabControl
    {
        static TabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControl), new FrameworkPropertyMetadata(typeof(TabControl)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(TabControl));
    }
}
