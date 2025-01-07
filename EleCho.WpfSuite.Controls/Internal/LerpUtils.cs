using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite.Internal
{
    internal static class LerpUtils
    {
        private static void RgbToHsv(int r, int g, int b, out float h, out float s, out float v)
        {
            // 将RGB值转换到0 - 1的范围
            float rf = r / 255.0f;
            float gf = g / 255.0f;
            float bf = b / 255.0f;

            float max = Math.Max(rf, Math.Max(gf, bf));
            float min = Math.Min(rf, Math.Min(gf, bf));

            v = max;

            // 计算饱和度
            if (max == 0)
            {
                s = 0;
            }
            else
            {
                s = (max - min) / max;
            }

            // 计算色相
            if (s == 0)
            {
                h = 0;
            }
            else
            {
                float delta = max - min;
                if (max == rf)
                {
                    h = ((gf - bf) / delta) % 6;
                }
                else if (max == gf)
                {
                    h = ((bf - rf) / delta) + 2;
                }
                else
                {
                    h = ((rf - gf) / delta) + 4;
                }
                h /= 6;
                if (h < 0)
                {
                    h += 1;
                }
            }
        }
        private static void HsvToRgb(float h, float s, float v, out byte r, out byte g, out byte b)
        {
            if (s == 0)
            {
                // 饱和度为0时，RGB值相等，都等于明度值乘以255
                byte value = (byte)(v * 255);
                r = value;
                g = value;
                b = value;
                return;
            }

            h *= 6;
            int i = (int)Math.Floor(h);
            float f = h - i;
            byte p = (byte)(v * (1 - s) * 255);
            byte q = (byte)(v * (1 - s * f) * 255);
            byte t = (byte)(v * (1 - s * (1 - f)) * 255);

            switch (i % 6)
            {
                case 0:
                    r = (byte)(v * 255);
                    g = t;
                    b = p;
                    break;
                case 1:
                    r = q;
                    g = (byte)(v * 255);
                    b = p;
                    break;
                case 2:
                    r = p;
                    g = (byte)(v * 255);
                    b = t;
                    break;
                case 3:
                    r = p;
                    g = q;
                    b = (byte)(v * 255);
                    break;
                case 4:
                    r = t;
                    g = p;
                    b = (byte)(v * 255);
                    break;
                case 5:
                    r = (byte)(v * 255);
                    g = p;
                    b = q;
                    break;
                default:
                    r = 0;
                    g = 0;
                    b = 0;
                    break;
            }
        }

        public static byte LerpByte(byte from, byte to, double t)
        {
            return (byte)(from + (to - from) * t);
        }

        public static float LerpSingle(float from, float to, double t)
        {
            return (float)(from + (to - from) * t);
        }

        public static double LerpDouble(double from, double to, double t)
        {
            return from + (to - from) * t;
        }

        public static Thickness LerpThickness(Thickness from, Thickness to, double t)
        {
            return new Thickness(
                LerpDouble(from.Left, to.Left, t),
                LerpDouble(from.Top, to.Top, t),
                LerpDouble(from.Right, to.Right, t),
                LerpDouble(from.Bottom, to.Bottom, t));
        }

        public static CornerRadius LerpCornerRadius(CornerRadius from, CornerRadius to, double t)
        {
            return new CornerRadius(
                LerpDouble(from.TopLeft, to.TopLeft, t),
                LerpDouble(from.TopRight, to.TopRight, t),
                LerpDouble(from.BottomRight, to.BottomRight, t),
                LerpDouble(from.BottomLeft, to.BottomLeft, t));
        }

        public static Color LerpColor(Color from, Color to, double t)
        {
            RgbToHsv(from.R, from.G, from.B, out float fromH, out float fromS, out float fromV);
            RgbToHsv(to.R, to.G, to.B, out float toH, out float toS, out float toV);

            HsvToRgb(
                LerpSingle(fromH, toH, t),
                LerpSingle(fromS, fromS, t),
                LerpSingle(fromV, toV, t),
                out var nowR,
                out var nowG,
                out var nowB);

            return Color.FromArgb(LerpByte(from.A, to.A, t), nowR, nowG, nowB);
        }

        public static Brush? LerpBrush(ref Brush? cache, Brush? from, Brush? to, double t)
        {
            // can not use from or to as cache
            if (cache == from ||
                cache == to)
            {
                cache = null;
            }

            if (from is null)
            {
                if (MathHelper.IsZero(t))
                {
                    return from;
                }

                from = Brushes.Transparent;
            }

            if (to is null)
            {
                if (MathHelper.IsZero(t - 1))
                {
                    return to;
                }

                to = Brushes.Transparent;
            }

            if (from is SolidColorBrush fromSolidColorBrush &&
                to is SolidColorBrush toSolidColorBrush)
            {
                if (cache is not SolidColorBrush cachedSolidColorBrush)
                {
                    cache = cachedSolidColorBrush = new SolidColorBrush();
                }

                cachedSolidColorBrush.Color = LerpColor(fromSolidColorBrush.Color, toSolidColorBrush.Color, t);
                return cachedSolidColorBrush;
            }

            if (cache is not VisualBrush cachedVisualBrush)
            {
                cache = cachedVisualBrush = new VisualBrush();
            }

            if (cachedVisualBrush.Visual is not Border rootBorder)
            {
                cachedVisualBrush.Visual = rootBorder = new Border();
            }

            if (rootBorder.Child is not Border contentBorder)
            {
                rootBorder.Child = contentBorder = new Border();
            }

            rootBorder.Width = 1;
            rootBorder.Height = 1;
            rootBorder.Background = from;
            contentBorder.Background = to;
            contentBorder.Opacity = t;

            return cachedVisualBrush;
        }
    }
}
