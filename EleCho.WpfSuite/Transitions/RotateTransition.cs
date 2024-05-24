using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    public class RotateTransition : ContentTransition
    {
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
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

        protected override Freezable CreateInstanceCore() => new RotateTransition();

        protected override Storyboard CreateNewContentStoryboard(UIElement container, UIElement newContent, bool forward)
        {
            if (newContent.RenderTransform is not RotateTransform)
                newContent.RenderTransform = new RotateTransform();
            newContent.RenderTransformOrigin = new Point(0.5f, 0.5f);

            DoubleAnimation rotateAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                From = -Angle,
                To = 0,
            };

            if (Reverse ^ !forward)
            {
                rotateAnimation.From = Angle;
            }

            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("RenderTransform.Angle"));

            return new Storyboard()
            {
                Duration = Duration,
                Children =
                {
                    rotateAnimation,
                }
            };
        }

        protected override Storyboard CreateOldContentStoryboard(UIElement container, UIElement oldContent, bool forward)
        {
            if (oldContent.RenderTransform is not RotateTransform)
                oldContent.RenderTransform = new RotateTransform();
            oldContent.RenderTransformOrigin = new Point(0.5f, 0.5f);

            DoubleAnimation rotateAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                To = Angle,
            };

            if (Reverse ^ !forward)
            {
                rotateAnimation.To = -Angle;
            }

            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("RenderTransform.Angle"));

            return new Storyboard()
            {
                Duration = Duration,
                Children =
                {
                    rotateAnimation,
                }
            };
        }

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(nameof(Angle), typeof(double), typeof(RotateTransition), new PropertyMetadata(90.0));

        public static readonly DependencyProperty ReverseProperty =
            DependencyProperty.Register(nameof(Reverse), typeof(bool), typeof(RotateTransition), new PropertyMetadata(false));

        public static readonly DependencyProperty TransformOriginProperty =
            DependencyProperty.Register(nameof(TransformOrigin), typeof(Point), typeof(RotateTransition), new PropertyMetadata(new Point(0, 0)));
    }
}
