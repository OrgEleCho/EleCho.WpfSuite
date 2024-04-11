using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace EleCho.WpfSuite
{
    public class ValueConverterGroup : ValueConverterBase<ValueConverterGroup>
    {
        public IEnumerable<IValueConverter>? Converters
        {
            get { return (IEnumerable<IValueConverter>)GetValue(ConvertersProperty); }
            set { SetValue(ConvertersProperty, value); }
        }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (Converters is null)
                return value;

            return Converters.Aggregate(value, (current, converter) => converter.Convert(value, targetType, parameter, culture));
        }

        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (Converters is null)
                return value;

            return Converters.Reverse().Aggregate(value, (current, converter) => converter.Convert(value, targetType, parameter, culture));
        }

        public static readonly DependencyProperty ConvertersProperty =
            DependencyProperty.Register(nameof(Converters), typeof(IEnumerable<IValueConverter>), typeof(ValueConverterGroup), new PropertyMetadata(null));
    }
}
