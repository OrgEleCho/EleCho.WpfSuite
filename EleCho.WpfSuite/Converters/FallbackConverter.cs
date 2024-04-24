using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class FallbackConverter : SingletonMultiValueConverterBase<FallbackConverter>
    {
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

