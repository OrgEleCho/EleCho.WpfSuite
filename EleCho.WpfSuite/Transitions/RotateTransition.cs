using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Rotate transition
    /// </summary>
    public class RotateTransition : ContentTransition
    {
        /// <summary>
        /// Angle of rotation
        /// </summary>
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
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
        protected override Freezable CreateInstanceCore() => new RotateTransition();

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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


        /// <summary>
        /// The DependencyProperty of <see cref="Angle"/> property
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(nameof(Angle), typeof(double), typeof(RotateTransition), new PropertyMetadata(90.0));

        /// <summary>
        /// The DependencyProperty of <see cref="Reverse"/> property
        /// </summary>
        public static readonly DependencyProperty ReverseProperty =
            DependencyProperty.Register(nameof(Reverse), typeof(bool), typeof(RotateTransition), new PropertyMetadata(false));

        /// <summary>
        /// The DependencyProperty of <see cref="TransformOrigin"/> property
        /// </summary>
        public static readonly DependencyProperty TransformOriginProperty =
            DependencyProperty.Register(nameof(TransformOrigin), typeof(Point), typeof(RotateTransition), new PropertyMetadata(new Point(0, 0)));
    }
}
