using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Slide transition
    /// </summary>
    public class SlideTransition : ContentTransition
    {
        /// <summary>
        /// Slide orientation
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Slide distance
        /// </summary>
        public double Distance
        {
            get { return (double)GetValue(DistanceProperty); }
            set { SetValue(DistanceProperty, value); }
        }

        /// <summary>
        /// Reverse this transition
        /// </summary>
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

        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore() => new SlideTransition();

        /// <inheritdoc/>
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

            if (Reverse ^ !forward)
            {
                translateAnimation.From *= -1;
            }

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
                    translateAnimation
                }
            };
        }

        /// <inheritdoc/>
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

            if (Reverse ^ !forward)
            {
                translateAnimation.To *= -1;
            }

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
                    translateAnimation
                }
            };
        }


        /// <summary>
        /// The DependencyProperty of <see cref="Orientation"/> property
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(SlideTransition), new PropertyMetadata(Orientation.Horizontal));

        /// <summary>
        /// The DependencyProperty of <see cref="Distance"/> property
        /// </summary>
        public static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register(nameof(Distance), typeof(double), typeof(SlideTransition), new PropertyMetadata(ValueBoxes.DoubleNaNBox));

        /// <summary>
        /// The DependencyProperty of <see cref="Reverse"/> property
        /// </summary>
        public static readonly DependencyProperty ReverseProperty =
            DependencyProperty.Register(nameof(Reverse), typeof(bool), typeof(SlideTransition), new PropertyMetadata(ValueBoxes.FalseBox));
    }
}
