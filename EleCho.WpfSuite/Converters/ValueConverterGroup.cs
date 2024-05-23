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
        static readonly Type s_typeObject = typeof(object);

        public List<IValueConverter> Converters { get; } = new();

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (Converters is null)
                return value;

            return Converters.Aggregate(value, (current, converter) => converter.Convert(current, s_typeObject, parameter, culture));
        }

        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (Converters is null)
                return value;

            return Converters.Reverse<IValueConverter>().Aggregate(value, (current, converter) => converter.Convert(current, s_typeObject, parameter, culture));
        }
    }
}
