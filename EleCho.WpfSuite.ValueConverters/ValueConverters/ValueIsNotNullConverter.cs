using System;
using System.Globalization;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Returns a Boolean value that indicates that the value is not null
    /// </summary>
    public class ValueIsNotNullConverter : SingletonValueConverterBase<ValueIsNotNullConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is not null;
        }
    }
}
