using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class EqualConverter : SingletonValueConverterBase<EqualConverter>
    {
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Equals(value, parameter);
        }
    }
}
