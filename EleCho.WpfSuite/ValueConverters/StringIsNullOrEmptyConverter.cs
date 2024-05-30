using System;
using System.Globalization;

namespace EleCho.WpfSuite
{
    public class StringIsNullOrEmptyConverter : SingletonValueConverterBase<StringIsNullOrEmptyConverter>
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string);
        }
    }
}
