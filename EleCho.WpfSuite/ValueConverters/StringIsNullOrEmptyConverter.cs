using System;
using System.Globalization;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Value converter that returns a Boolean value that indicates that the string is null or empty
    /// </summary>
    public class StringIsNullOrEmptyConverter : SingletonValueConverterBase<StringIsNullOrEmptyConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string);
        }
    }
}
