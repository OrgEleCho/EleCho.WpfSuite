using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Convert color to it's brightness (float value 0~1)
    /// </summary>
    public class ColorBrightnessConverter : ValueConverterBase<ColorBrightnessConverter, Color, float>
    {
        public override float Convert(Color value, Type targetType, object? parameter, CultureInfo culture)
        {
            var brightness = value.ScR * 0.3f + value.ScG * 0.59f + value.ScB * 0.11f;
            if (brightness > 1)
            {
                brightness = 1;
            }

            return brightness;
        }
    }
}

