using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Fallback value between multiple bindings
    /// </summary>
    public class FallbackConverter : SingletonMultiValueConverterBase<FallbackConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
        {
            foreach (var value in values)
            {
                if (value is not null &&
                    value != DependencyProperty.UnsetValue)
                    return value;
            }

            return null;
        }
    }
}

