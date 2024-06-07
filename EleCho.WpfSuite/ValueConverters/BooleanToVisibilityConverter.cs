using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Convert boolean to visibility
    /// </summary>
    public class BooleanToVisibilityConverter : BooleanToValueConverter<Visibility>
    {
        static BooleanToVisibilityConverter()
        {
            ValueWhenTrueProperty.OverrideMetadata(typeof(BooleanToVisibilityConverter), new PropertyMetadata(Visibility.Visible));
            ValueWhenFalseProperty.OverrideMetadata(typeof(BooleanToVisibilityConverter), new PropertyMetadata(Visibility.Collapsed));
        }
    }
}

