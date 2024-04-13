using System;
using System.Globalization;

namespace EleCho.WpfSuite
{
    public class StringIsNotNullOrWhiteSpaceConverter : SingletonValueConverterBase<StringIsNotNullOrWhiteSpaceConverter>
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value as string);
        }
    }
}
