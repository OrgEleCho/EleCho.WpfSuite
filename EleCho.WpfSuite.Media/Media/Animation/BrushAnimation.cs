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
        private VisualBrush? _workingBrush;

        /// <inheritdoc/>
        public override Type TargetPropertyType
        {
            get
            {
                return typeof(Brush);
            }
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
                return Brushes.Transparent;

            //use the standard values if From and To are not set 
            //(it is the value of the given property)
            defaultOriginValue = this.From ?? defaultOriginValue;
            defaultDestinationValue = this.To ?? defaultDestinationValue;

            if (animationClock.CurrentProgress.Value == 0)
                return defaultOriginValue;
            if (animationClock.CurrentProgress.Value == 1)
                return defaultDestinationValue;

            var workingBrush = _workingBrush ??= new VisualBrush(new Grid()
            {
                Width = 1,
                Height = 1,
                Children =
                {
                    new Border()
                    {
                        Background = defaultOriginValue,
                    },
                    new Border()
                    {
                        Background = defaultOriginValue,
                    }
                }
            });

            var workingBrushPanel = (Grid)workingBrush.Visual;
            var fromBrushBorder = workingBrushPanel.Children[0];
            var toBrushBorder = workingBrushPanel.Children[1];

            fromBrushBorder.Opacity = 1 - animationClock.CurrentProgress.Value;
            toBrushBorder.Opacity = animationClock.CurrentProgress.Value;

            return workingBrush;
        }

        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore()
        {
            return new BrushAnimation();
        }


        /// <summary>
        /// From Brush
        /// </summary>
        public Brush From
        {
            get { return (Brush)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        /// <summary>
        /// To Brush
        /// </summary>
        public Brush To
        {
            get { return (Brush)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
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
    }
}
