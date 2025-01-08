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
