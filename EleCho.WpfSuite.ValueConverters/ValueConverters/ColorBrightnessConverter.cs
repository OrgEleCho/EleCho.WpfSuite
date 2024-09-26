using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite.ValueConverters
{
    /// <summary>
    /// Convert color to it's brightness (float number 0~1)
    /// </summary>
    public class ColorBrightnessConverter : ValueConverterBase<ColorBrightnessConverter, Color, float>
    {
        /// <summary>
        /// Get brightness of a color
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float GetBrightness(float r, float g, float b)
        {
            // 将 RGB 值转换为 YUV 值
            float y = 0.299f * r + 0.587f * g + 0.114f * b;

            // 计算亮度值
            return y;
        }

        /// <inheritdoc/>
        public override float Convert(Color value, Type targetType, object? parameter, CultureInfo culture)
        {
            var brightness = GetBrightness(value.ScR, value.ScG, value.ScB);

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

