using System.Windows;

namespace EleCho.WpfSuite
{
    public class BooleanToVisibilityConverter : BooleanToValueConverter<Visibility>
    {
        public BooleanToVisibilityConverter()
        {
            ValueWhenTrue = Visibility.Visible;
            ValueWhenFalse = Visibility.Collapsed;
        }
    }
}

