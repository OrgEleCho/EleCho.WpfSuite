using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    public class SlideFadeTransition : ContentTransition
    {
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public double Distance
        {
            get { return (double)GetValue(DistanceProperty); }
            set { SetValue(DistanceProperty, value); }
        }

        public bool Reverse
        {
            get { return (bool)GetValue(ReverseProperty); }
            set { SetValue(ReverseProperty, value); }
        }

        private double GetDistance(UIElement container)
        {
            var distance = 10.0;
            if (container is FrameworkElement frameworkElement)
            {
                distance = Orientation == Orientation.Horizontal ?
                    frameworkElement.ActualWidth :
                    frameworkElement.ActualHeight;
            }

            if (Distance is double customDistance &&
                !double.IsNaN(customDistance))
            {
                distance = customDistance;
            }

            return distance;
        }

        protected override Freezable CreateInstanceCore() => new SlideFadeTransition();

        protected override Storyboard CreateNewContentStoryboard(UIElement container, UIElement newContent, bool forward)
        {
            if (newContent.RenderTransform is not TranslateTransform)
                newContent.RenderTransform = new TranslateTransform();

            var distance = GetDistance(container);
            DoubleAnimation translateAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                From = distance,
                To = 0,
            };

            DoubleAnimation opacityAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                From = 0,
                To = 1
            };

            if (Reverse ^ !forward)
            {
                translateAnimation.From *= -1;
            }

            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(nameof(FrameworkElement.Opacity)));

            if (Orientation == Orientation.Horizontal)
            {
                Storyboard.SetTargetProperty(translateAnimation, new PropertyPath("RenderTransform.X"));
            }
            else
            {
                Storyboard.SetTargetProperty(translateAnimation, new PropertyPath("RenderTransform.Y"));
            }

            return new Storyboard()
            {
                Duration = Duration,
                Children =
                {
                    translateAnimation,
                    opacityAnimation
                }
            };
        }

        protected override Storyboard CreateOldContentStoryboard(UIElement container, UIElement oldContent, bool forward)
        {
            if (oldContent.RenderTransform is not TranslateTransform)
                oldContent.RenderTransform = new TranslateTransform();

            var distance = GetDistance(container);
            DoubleAnimation translateAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                To = -distance,
            };

            DoubleAnimation opacityAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                To = 0
            };

            if (Reverse ^ !forward)
            {
                translateAnimation.To *= -1;
            }

            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(nameof(FrameworkElement.Opacity)));

            if (Orientation == Orientation.Horizontal)
            {
                Storyboard.SetTargetProperty(translateAnimation, new PropertyPath("RenderTransform.X"));
            }
            else
            {
                Storyboard.SetTargetProperty(translateAnimation, new PropertyPath("RenderTransform.Y"));
            }

            return new Storyboard()
            {
                Duration = Duration,
                Children =
                {
                    translateAnimation,
                    opacityAnimation
                }
            };
        }


        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(SlideFadeTransition), new PropertyMetadata(Orientation.Horizontal));

        public static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register(nameof(Distance), typeof(double), typeof(SlideFadeTransition), new PropertyMetadata(ValueBoxes.DoubleNaNBox));

        public static readonly DependencyProperty ReverseProperty =
            DependencyProperty.Register(nameof(Reverse), typeof(bool), typeof(SlideFadeTransition), new PropertyMetadata(ValueBoxes.FalseBox));
    }
}
