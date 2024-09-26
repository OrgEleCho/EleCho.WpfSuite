using System.Windows;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Convert boolean to visibility
    /// </summary>
    public class BooleanToVisibilityConverter : BooleanToValueConverter<BooleanToVisibilityConverter, Visibility>
    {
        static BooleanToVisibilityConverter()
        {
            ValueWhenTrueProperty.OverrideMetadata(typeof(BooleanToVisibilityConverter), new PropertyMetadata(Visibility.Visible));
            ValueWhenFalseProperty.OverrideMetadata(typeof(BooleanToVisibilityConverter), new PropertyMetadata(Visibility.Collapsed));
        }
    }
}

