using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Value converter that returns a boolean value that indicates that the value equals parameter
    /// </summary>
    public class EqualConverter : SingletonValueConverterBase<EqualConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Equals(value, parameter);
        }
    }
}
