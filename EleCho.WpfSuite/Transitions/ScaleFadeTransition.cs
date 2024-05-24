using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    public class ScaleFadeTransition : ContentTransition
    {
        public double LargeScale
        {
            get { return (double)GetValue(LargeScaleProperty); }
            set { SetValue(LargeScaleProperty, value); }
        }

        public double SmallScale
        {
            get { return (double)GetValue(SmallScaleProperty); }
            set { SetValue(SmallScaleProperty, value); }
        }

        public bool Reverse
        {
            get { return (bool)GetValue(ReverseProperty); }
            set { SetValue(ReverseProperty, value); }
        }

        public Point TransformOrigin
        {
            get { return (Point)GetValue(TransformOriginProperty); }
            set { SetValue(TransformOriginProperty, value); }
        }

        protected override Freezable CreateInstanceCore() => new ScaleFadeTransition();

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

        public static readonly DependencyProperty ReverseProperty =
            DependencyProperty.Register(nameof(Reverse), typeof(bool), typeof(ScaleFadeTransition), new PropertyMetadata(false));

        public static readonly DependencyProperty LargeScaleProperty =
            DependencyProperty.Register(nameof(LargeScale), typeof(double), typeof(ScaleFadeTransition), new PropertyMetadata(1.25));

        public static readonly DependencyProperty SmallScaleProperty =
            DependencyProperty.Register(nameof(SmallScale), typeof(double), typeof(ScaleFadeTransition), new PropertyMetadata(0.75));

        public static readonly DependencyProperty TransformOriginProperty =
            DependencyProperty.Register(nameof(TransformOrigin), typeof(Point), typeof(ScaleFadeTransition), new PropertyMetadata(new Point(0.5, 0.5)));
    }
}
