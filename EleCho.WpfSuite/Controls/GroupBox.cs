using System.Windows;

namespace EleCho.WpfSuite
{
    public class GroupBox : System.Windows.Controls.GroupBox
    {
        static GroupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupBox), new FrameworkPropertyMetadata(typeof(GroupBox)));
        }


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }


        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(GroupBox), new FrameworkPropertyMetadata(new CornerRadius(3)));
    }
}
