using System;
using System.Globalization;
using System.Windows;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Convert number to <see cref="CornerRadius"/>
    /// </summary>
    public class NumberToCornerRadiusConverter : SingletonValueConverterBase<NumberToCornerRadiusConverter>
    {
        /// <inheritdoc/>
        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            try
            {
                var current = System.Convert.ToDouble(value);

                return new CornerRadius(current);
            }
            catch
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
