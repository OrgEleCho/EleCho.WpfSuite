using System;
using System.Globalization;

namespace EleCho.WpfSuite
{
    public class NotEqualConverter : SingletonValueConverterBase<NotEqualConverter>
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !Equals(value, parameter);
        }
    }
}
