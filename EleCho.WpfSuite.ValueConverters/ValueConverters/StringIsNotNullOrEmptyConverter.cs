using System;
using System.Globalization;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Value converter that returns a Boolean value that indicates that the string is not null or empty
    /// </summary>
    public class StringIsNotNullOrEmptyConverter : SingletonValueConverterBase<StringIsNotNullOrEmptyConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !string.IsNullOrEmpty(value as string);
        }
    }
}
