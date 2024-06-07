using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Convert value with specified value converters
    /// </summary>
    [ContentProperty(nameof(Converters))]
    public class ValueConverterGroup : ValueConverterBase<ValueConverterGroup>
    {
        static readonly Type s_typeObject = typeof(object);

        /// <summary>
        /// Value converters to use
        /// </summary>
        public List<IValueConverter> Converters { get; } = new();

        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (Converters is null)
                return value;

            for (int i = 0; i < Converters.Count; i++)
            {
                IValueConverter? converter = Converters[i];

                var isLast = i == Converters.Count - 1;
                var currentTargetType = isLast ? targetType : s_typeObject;

                value = converter.Convert(value, currentTargetType, parameter, culture);
            }

            return value;
        }

        /// <inheritdoc/>
        public override object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (Converters is null)
                return value;

            for (int i = Converters.Count - 1; i >= 0; i--)
            {
                IValueConverter? converter = Converters[i];

                var isFirst = i == 0;
                var currentTargetType = isFirst ? targetType : s_typeObject;

                value = converter.Convert(value, currentTargetType, parameter, culture);
            }

            return value;
        }
    }
}
