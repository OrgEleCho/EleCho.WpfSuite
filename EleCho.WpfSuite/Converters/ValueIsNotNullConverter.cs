using System;
using System.Globalization;

namespace EleCho.WpfSuite
{
    public class ValueIsNotNullConverter : SingletonValueConverterBase<ValueIsNotNullConverter>
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is not null;
        }
    }
}
