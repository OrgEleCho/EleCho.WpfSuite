using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    public class InvertBooleanConverter : SingletonValueConverterBase<InvertBooleanConverter>
    {
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
    }
}
