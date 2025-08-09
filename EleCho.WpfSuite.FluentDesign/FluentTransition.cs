using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using EleCho.WpfSuite.Media.Transition;

namespace EleCho.WpfSuite.FluentDesign
{
    public class FluentTransition : StoryboardContentTransition
    {
        static FluentTransition()
        {
            DurationProperty.OverrideMetadata(typeof(FluentTransition), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(300)), null, CoerceDuration));
        }

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

        protected override Freezable CreateInstanceCore() => new FluentTransition();
        protected override Storyboard CreateNewContentStoryboard(UIElement container, UIElement newContent, bool forward)
        {
            if (newContent.RenderTransform is not TranslateTransform)
                newContent.RenderTransform = new TranslateTransform();

            DoubleAnimationUsingKeyFrames opacityAnimation = new()
            {
                Duration = Duration,
                KeyFrames =
                {
                    new DiscreteDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.Zero)),
                    
#if NET6_0_OR_GREATER
                    new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(Duration.TimeSpan * 0.4)),
#else
                    new DiscreteDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds * 0.4))),
#endif
                },
            };

            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(nameof(UIElement.Opacity)));

            var distance = GetDistance(container);
            DoubleAnimation translateAnimation = new()
            {
                EasingFunction = EasingFunction,
#if NET6_0_OR_GREATER
                BeginTime = Duration.TimeSpan * 0.4,
                Duration = new Duration(Duration.TimeSpan * 0.6),
#else
                BeginTime = TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds * 0.4),
                Duration = new Duration(TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds * 0.6)),
#endif
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
                    translateAnimation,
                    opacityAnimation,
                }
            };
        }

        protected override Storyboard CreateOldContentStoryboard(UIElement container, UIElement oldContent, bool forward)
        {
            DoubleAnimation opacityAnimation = new()
            {
                EasingFunction = EasingFunction,
#if NET6_0_OR_GREATER
                Duration = new Duration(Duration.TimeSpan * 0.4),
#else
                Duration = new Duration(TimeSpan.FromMilliseconds(Duration.TimeSpan.TotalMilliseconds * 0.4)),
#endif
                To = 0,
            };

            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(nameof(FrameworkElement.Opacity)));

            return new Storyboard()
            {
                Duration = Duration,
                Children =
                {
                    opacityAnimation
                }
            };
        }


        /// <summary>
        /// The DependencyProperty of <see cref="Orientation"/> property
        /// </summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(FluentTransition), new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        /// The DependencyProperty of <see cref="Distance"/> property
        /// </summary>
        public static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register(nameof(Distance), typeof(double), typeof(FluentTransition), new PropertyMetadata(double.NaN));

        /// <summary>
        /// The DependencyProperty of <see cref="Reverse"/> property
        /// </summary>
        public static readonly DependencyProperty ReverseProperty =
            DependencyProperty.Register(nameof(Reverse), typeof(bool), typeof(FluentTransition), new PropertyMetadata(false));
    }
}
