using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite.Media.Transition
{
    /// <summary>
    /// Scale and fade transition
    /// </summary>
    public class ScaleFadeTransition : ContentTransition
    {
        /// <summary>
        /// Large scale
        /// </summary>
        public double LargeScale
        {
            get { return (double)GetValue(LargeScaleProperty); }
            set { SetValue(LargeScaleProperty, value); }
        }

        /// <summary>
        /// Small scale
        /// </summary>
        public double SmallScale
        {
            get { return (double)GetValue(SmallScaleProperty); }
            set { SetValue(SmallScaleProperty, value); }
        }

        /// <summary>
        /// Reverse this transition
        /// </summary>
        public bool Reverse
        {
            get { return (bool)GetValue(ReverseProperty); }
            set { SetValue(ReverseProperty, value); }
        }

        /// <summary>
        /// Transform origin of content
        /// </summary>
        public Point TransformOrigin
        {
            get { return (Point)GetValue(TransformOriginProperty); }
            set { SetValue(TransformOriginProperty, value); }
        }

        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore() => new ScaleFadeTransition();

        /// <inheritdoc/>
        protected override Storyboard CreateNewContentStoryboard(UIElement container, UIElement newContent, bool forward)
        {
            if (newContent.RenderTransform is not ScaleTransform)
                newContent.RenderTransform = new ScaleTransform();
            newContent.RenderTransformOrigin = TransformOrigin;

            DoubleAnimation scaleXAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                From = LargeScale,
                To = 1,
            };

            DoubleAnimation scaleYAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                From = LargeScale,
                To = 1,
            };

            DoubleAnimation opacityAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                From = 0,
                To = 1,
            };

            if (Reverse ^ !forward)
            {
                scaleXAnimation.From = SmallScale;
                scaleYAnimation.From = SmallScale;
            }

            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(nameof(FrameworkElement.Opacity)));

            return new Storyboard()
            {
                Duration = Duration,
                Children =
                {
                    scaleXAnimation,
                    scaleYAnimation,
                    opacityAnimation
                }
            };
        }

        /// <inheritdoc/>
        protected override Storyboard CreateOldContentStoryboard(UIElement container, UIElement oldContent, bool forward)
        {
            if (oldContent.RenderTransform is not ScaleTransform)
                oldContent.RenderTransform = new ScaleTransform();
            oldContent.RenderTransformOrigin = TransformOrigin;

            DoubleAnimation scaleXAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                To = SmallScale,
            };

            DoubleAnimation scaleYAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                To = SmallScale,
            };

            DoubleAnimation opacityAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                To = 0,
            };

            if (Reverse ^ !forward)
            {
                scaleXAnimation.To = LargeScale;
                scaleYAnimation.To = LargeScale;
            }

            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(nameof(FrameworkElement.Opacity)));

            return new Storyboard()
            {
                Duration = Duration,
                Children =
                {
                    scaleXAnimation,
                    scaleYAnimation,
                    opacityAnimation
                }
            };
        }

        /// <summary>
        /// The DependencyProperty of <see cref="Reverse"/> property
        /// </summary>
        public static readonly DependencyProperty ReverseProperty =
            DependencyProperty.Register(nameof(Reverse), typeof(bool), typeof(ScaleFadeTransition), new PropertyMetadata(false));

        /// <summary>
        /// The DependencyProperty of <see cref="LargeScale"/> property
        /// </summary>
        public static readonly DependencyProperty LargeScaleProperty =
            DependencyProperty.Register(nameof(LargeScale), typeof(double), typeof(ScaleFadeTransition), new PropertyMetadata(1.25));

        /// <summary>
        /// The DependencyProperty of <see cref="SmallScale"/> property
        /// </summary>
        public static readonly DependencyProperty SmallScaleProperty =
            DependencyProperty.Register(nameof(SmallScale), typeof(double), typeof(ScaleFadeTransition), new PropertyMetadata(0.75));

        /// <summary>
        /// The DependencyProperty of <see cref="TransformOrigin"/> property
        /// </summary>
        public static readonly DependencyProperty TransformOriginProperty =
            DependencyProperty.Register(nameof(TransformOrigin), typeof(Point), typeof(ScaleFadeTransition), new PropertyMetadata(new Point(0.5, 0.5)));
    }
}
