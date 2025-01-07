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

        private static byte LerpByte(byte from, byte to, double t)
        {
            return (byte)(from + (to - from) * t);
        }

        private static float LerpSingle(float from, float to, double t)
        {
            return (float)(from + (to - from) * t);
        }

        private static Color LerpColor(Color from, Color to, double t)
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

            // can not use from or to as cache
            if (_workingBrush == from ||
                _workingBrush == to)
            {
                _workingBrush = null;
            }

            if (MathHelper.IsZero(progress))
            {
                return from;
            }
            else if (MathHelper.IsZero(progress - 1))
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
                if (_workingBrush is not SolidColorBrush cachedSolidColorBrush)
                {
                    _workingBrush = cachedSolidColorBrush = new SolidColorBrush();
                }

                cachedSolidColorBrush.Color = LerpColor(fromSolidColorBrush.Color, toSolidColorBrush.Color, progress);
                return cachedSolidColorBrush;
            }

            if (_workingBrush is not VisualBrush cachedVisualBrush)
            {
                _workingBrush = cachedVisualBrush = new VisualBrush();
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
            contentBorder.Opacity = progress;

            return cachedVisualBrush;
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
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
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
