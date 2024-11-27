using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Value converter that returns the reciprocal of number
    /// </summary>
    public class InvertNumberConverter : SingletonValueConverterBase<InvertNumberConverter, double, double>
    {
        /// <inheritdoc/>
        public override double Convert(double value, Type targetType, object? parameter, CultureInfo culture)
        {
            return 1 / value;
        }

        /// <inheritdoc/>
        public override double ConvertBack(double value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
