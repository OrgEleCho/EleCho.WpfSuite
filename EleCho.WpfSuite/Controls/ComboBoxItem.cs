using System.Windows;

namespace EleCho.WpfSuite
{
    public class ComboBoxItem : System.Windows.Controls.ComboBoxItem
    {
        static ComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBoxItem), new FrameworkPropertyMetadata(typeof(ComboBoxItem)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }


        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(ComboBoxItem));


    }
}
