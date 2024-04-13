using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace EleCho.WpfSuite
{
    [ContentProperty(nameof(Converters))]
    public class ValueConverterGroup : ValueConverterBase<ValueConverterGroup>
    {
        public List<IValueConverter> Converters
        {
            get { return (List<IValueConverter>)GetValue(ConvertersProperty); }
            set { SetValue(ConvertersProperty, value); }
        }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (Converters is null)
                return value;

            return Converters.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
        }

        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (Converters is null)
                return value;

            return Converters.Reverse<IValueConverter>().Aggregate(value, (current, converter) => converter.Convert(value, targetType, parameter, culture));
        }

        public static readonly DependencyProperty ConvertersProperty =
            DependencyProperty.Register(nameof(Converters), typeof(List<IValueConverter>), typeof(ValueConverterGroup), new PropertyMetadata(new List<IValueConverter>()));
    }
}
