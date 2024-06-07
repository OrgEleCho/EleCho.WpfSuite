using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Invert a boolean value
    /// </summary>
    public class InvertBooleanConverter : SingletonValueConverterBase<InvertBooleanConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                var current = System.Convert.ToBoolean(value);
                return !current;
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }

        /// <inheritdoc/>
        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
