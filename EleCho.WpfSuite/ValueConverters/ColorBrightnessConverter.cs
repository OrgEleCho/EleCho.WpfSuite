using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Convert color to it's brightness (float number 0~1)
    /// </summary>
    public class ColorBrightnessConverter : ValueConverterBase<ColorBrightnessConverter, Color, float>
    {
        /// <inheritdoc/>
        public override float Convert(Color value, Type targetType, object? parameter, CultureInfo culture)
        {
            var brightness = ColorUtils.GetBrightness(value.ScR, value.ScG, value.ScB);

            if (brightness < 0)
            {
                brightness = 0;
            }

            if (brightness > 1)
            {
                brightness = 1;
            }

            return brightness;
        }
    }
}

