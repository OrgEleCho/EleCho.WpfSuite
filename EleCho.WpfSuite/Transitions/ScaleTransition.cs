using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    public class ScaleTransition : ContentTransition
    {
        protected override Storyboard CreateNewContentStoryboard(UIElement container, UIElement newContent, bool forward)
        {
            if (newContent.RenderTransform is not ScaleTransform)
                newContent.RenderTransform = new ScaleTransform();
            newContent.RenderTransformOrigin = new Point(0.5f, 0.5f);

            DoubleAnimation scaleXAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                From = 1.25,
                To = 1,
            };

            DoubleAnimation scaleYAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                From = 1.25,
                To = 1,
            };

            DoubleAnimation opacityAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                From = 0,
                To = 1,
            };

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
            oldContent.RenderTransformOrigin = new Point(0.5f, 0.5f);

            DoubleAnimation scaleXAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                To = 0.75,
            };

            DoubleAnimation scaleYAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                To = 0.75,
            };

            DoubleAnimation opacityAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                To = 0,
            };

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


        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(SlideTransition), new PropertyMetadata(Orientation.Horizontal));
    }
}
