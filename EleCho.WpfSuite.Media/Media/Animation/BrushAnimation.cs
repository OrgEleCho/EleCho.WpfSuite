using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite.Media.Animation
{
    /// <summary>
    /// Brush to Brush animation
    /// </summary>
    public class BrushAnimation : AnimationTimeline
    {
        private Brush? _workingBrush;

        /// <inheritdoc/>
        public override Type TargetPropertyType
        {
            get
            {
                return typeof(Brush);
            }
        }

        private static byte LerpByte(byte from, byte to, double t)
        {
            return (byte)(from + (to - from) * t);
        }

        private static Color LerpColor(Color from, Color to, double t)
        {
            return Color.FromArgb(
                LerpByte(from.A, to.A, t),
                LerpByte(from.R, to.R, t),
                LerpByte(from.G, to.G, t),
                LerpByte(from.B, to.B, t));
        }

        private static bool IsTransparent(Brush? brush)
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

        private static bool MayBeNotOpaque(Brush? brush)
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

        private static bool MustBeOpaque(Brush? brush)
        {
            return !MayBeNotOpaque(brush);
        }

        private static Brush? LerpBrush(ref Brush? cache, Brush? from, Brush? to, double t)
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
                if (MustBeOpaque(fromSolidColorBrush) &&
                    MustBeOpaque(toSolidColorBrush))
                {
                    if (cache is not SolidColorBrush cachedSolidColorBrush)
                    {
                        cache = cachedSolidColorBrush = new SolidColorBrush();
                    }

                    cachedSolidColorBrush.Color = LerpColor(fromSolidColorBrush.Color, toSolidColorBrush.Color, t);
                    return cachedSolidColorBrush;
                }
                else if (IsTransparent(fromSolidColorBrush))
                {
                    if (cache is not SolidColorBrush cachedSolidColorBrush)
                    {
                        cache = cachedSolidColorBrush = new SolidColorBrush();
                    }

                    var fromColor = Color.FromArgb(0, toSolidColorBrush.Color.R, toSolidColorBrush.Color.G, toSolidColorBrush.Color.B);
                    cachedSolidColorBrush.Color = LerpColor(fromColor, toSolidColorBrush.Color, t);
                    return cachedSolidColorBrush;
                }
                else if (IsTransparent(toSolidColorBrush))
                {
                    if (cache is not SolidColorBrush cachedSolidColorBrush)
                    {
                        cache = cachedSolidColorBrush = new SolidColorBrush();
                    }

                    var toColor = Color.FromArgb(0, fromSolidColorBrush.Color.R, fromSolidColorBrush.Color.G, fromSolidColorBrush.Color.B);
                    cachedSolidColorBrush.Color = LerpColor(fromSolidColorBrush.Color, toColor, t);
                    return cachedSolidColorBrush;
                }
            }
            else
            {
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
            }

            if (cache is not VisualBrush finalCachedVisualBrush)
            {
                cache = finalCachedVisualBrush = new VisualBrush();
            }

            if (finalCachedVisualBrush.Visual is not Grid rootGrid)
            {
                finalCachedVisualBrush.Visual = rootGrid = new Grid();
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

            return finalCachedVisualBrush;
        }

        /// <inheritdoc/>
        public override object? GetCurrentValue(object defaultOriginValue,
                                               object defaultDestinationValue,
                                               AnimationClock animationClock)
        {
            return GetCurrentValue(defaultOriginValue as Brush,
                                   defaultDestinationValue as Brush,
                                   animationClock);
        }

        /// <inheritdoc/>
        public object? GetCurrentValue(Brush? defaultOriginValue,
                                      Brush? defaultDestinationValue,
                                      AnimationClock animationClock)
        {
            if (!animationClock.CurrentProgress.HasValue)
                return null;

            //use the standard values if From and To are not set 
            //(it is the value of the given property)
            var from = this.From ?? defaultOriginValue;
            var to = this.To ?? defaultDestinationValue;

            var progress = animationClock.CurrentProgress.Value;

            if (EasingFunction is IEasingFunction easingFunction)
            {
                progress = easingFunction.Ease(progress);
            }

            return LerpBrush(ref _workingBrush, from, to, progress);
        }

        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore()
        {
            return new BrushAnimation();
        }


        /// <summary>
        /// From Brush
        /// </summary>
        public Brush? From
        {
            get { return (Brush?)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        /// <summary>
        /// To Brush
        /// </summary>
        public Brush? To
        {
            get { return (Brush?)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        /// <summary>
        /// EasingFunction
        /// </summary>
        public IEasingFunction? EasingFunction
        {
            get { return (IEasingFunction?)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        /// <summary>
        /// DependencyProperty of From property
        /// </summary>
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register(nameof(From), typeof(Brush), typeof(BrushAnimation), new PropertyMetadata(null));

        /// <summary>
        /// DependencyProperty of To property
        /// </summary>
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(nameof(To), typeof(Brush), typeof(BrushAnimation), new PropertyMetadata(null));

        /// <summary>
        /// DependencyProperty of EasingFunction property
        /// </summary>
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register(nameof(EasingFunction), typeof(IEasingFunction), typeof(BrushAnimation), new PropertyMetadata(null));
    }
}
