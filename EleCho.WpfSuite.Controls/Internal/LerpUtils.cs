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

        public static bool IsTransparent(Brush? brush)
        {
            if (brush is null)
            {
                return true;
            }

            if (brush is SolidColorBrush solidColorBrush)
            {
                return solidColorBrush.Color.A == 0;
            }

            return false;
        }

        public static bool MayBeNotOpaque(Brush? brush)
        {
            if (brush == null)
            {
                return true;
            }

            if (brush is SolidColorBrush solidColorBrush)
            {
                return solidColorBrush.Color.A != 255;
            }
            else if (brush is GradientBrush gradientBrush)
            {
                foreach (var stop in gradientBrush.GradientStops)
                {
                    if (stop.Color.A != 255)
                    {
                        return true;
                    }
                }

                return false;
            }
            else if (brush is BitmapCacheBrush bitmapCacheBrush)
            {
                return true;
            }
            else if (brush is TileBrush tileBrush)
            {
                return true;
            }

            return false;
        }

        public static bool MustBeOpaque(Brush? brush)
        {
            return !MayBeNotOpaque(brush);
        }

        public static Brush? LerpBrush(ref Brush? cache, Brush? from, Brush? to, double t)
        {
            // can not use from or to as cache
            if (cache == from ||
                cache == to)
            {
                cache = null;
            }

            if (MathHelper.IsZero(t))
            {
                return from;
            }
            else if (MathHelper.IsZero(t - 1))
            {
                return to;
            }

            if (from is null)
            {
                from = Brushes.Transparent;
            }

            if (to is null)
            {
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

            if (MustBeOpaque(to))
            {
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
            else if (IsTransparent(from))
            {
                if (cache is not VisualBrush cachedVisualBrush)
                {
                    cache = cachedVisualBrush = new VisualBrush();
                }

                if (cachedVisualBrush.Visual is not Border rootBorder)
                {
                    cachedVisualBrush.Visual = rootBorder = new Border();
                }

                rootBorder.Child = null;
                rootBorder.Width = 1;
                rootBorder.Height = 1;
                rootBorder.Background = to;
                rootBorder.Opacity = t;

                return cachedVisualBrush;
            }
            else if (IsTransparent(to))
            {
                if (cache is not VisualBrush cachedVisualBrush)
                {
                    cache = cachedVisualBrush = new VisualBrush();
                }

                if (cachedVisualBrush.Visual is not Border rootBorder)
                {
                    cachedVisualBrush.Visual = rootBorder = new Border();
                }

                rootBorder.Child = null;
                rootBorder.Width = 1;
                rootBorder.Height = 1;
                rootBorder.Background = from;
                rootBorder.Opacity = 1 - t;

                return cachedVisualBrush;
            }
            else
            {
                if (cache is not VisualBrush cachedVisualBrush)
                {
                    cache = cachedVisualBrush = new VisualBrush();
                }

                if (cachedVisualBrush.Visual is not Grid rootGrid)
                {
                    cachedVisualBrush.Visual = rootGrid = new Grid();
                }

                var childBorder1 = default(Border);
                var childBorder2 = default(Border);

                if (rootGrid.Children.Count < 1)
                {
                    rootGrid.Children.Add(childBorder1 = new Border());
                }
                else
                {
                    childBorder1 = rootGrid.Children[0] as Border;
                    if (childBorder1 == null)
                    {
                        rootGrid.Children[0] = childBorder1 = new Border();
                    }
                }

                if (rootGrid.Children.Count < 2)
                {
                    rootGrid.Children.Add(childBorder2 = new Border());
                }
                else
                {
                    childBorder2 = rootGrid.Children[1] as Border;
                    if (childBorder2 == null)
                    {
                        rootGrid.Children[1] = childBorder2 = new Border();
                    }
                }

                while (rootGrid.Children.Count > 2)
                {
                    rootGrid.Children.RemoveAt(rootGrid.Children.Count - 1);
                }

                rootGrid.Width = 1;
                rootGrid.Height = 1;
                childBorder1.Background = from;
                childBorder1.Opacity = 1 - t;
                childBorder2.Background = to;
                childBorder2.Opacity = t;

                return cachedVisualBrush;
            }
        }
    }
}
