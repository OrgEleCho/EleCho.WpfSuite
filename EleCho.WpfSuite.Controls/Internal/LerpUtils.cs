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
            return Color.FromArgb(
                LerpByte(from.A, to.A, t),
                LerpByte(from.R, to.R, t),
                LerpByte(from.G, to.G, t),
                LerpByte(from.B, to.B, t));
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
