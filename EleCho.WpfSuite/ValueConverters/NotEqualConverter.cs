using System;
using System.Globalization;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Value converter that returns a boolean value that indicates that the value not equals parameter
    /// </summary>
    public class NotEqualConverter : SingletonValueConverterBase<NotEqualConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !Equals(value, parameter);
        }
    }
}
