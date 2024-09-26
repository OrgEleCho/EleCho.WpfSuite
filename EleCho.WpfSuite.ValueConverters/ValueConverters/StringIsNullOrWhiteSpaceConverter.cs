using System;
using System.Globalization;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Value converter that returns a Boolean value that indicates that the string is null or whitespace
    /// </summary>
    public class StringIsNullOrWhiteSpaceConverter : SingletonValueConverterBase<StringIsNullOrWhiteSpaceConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value as string);
        }
    }
}
