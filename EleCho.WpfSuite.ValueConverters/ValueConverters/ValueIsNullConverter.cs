using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Returns a Boolean value that indicates that the value is null
    /// </summary>
    public class ValueIsNullConverter : SingletonValueConverterBase<ValueIsNullConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is null;
        }
    }
}
