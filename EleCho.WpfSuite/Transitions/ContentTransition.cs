using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// ContentTransition
    /// </summary>
    public abstract class ContentTransition : Freezable, IContentTransition
    {
        /// <summary>
        /// Create an instance of this Transition
        /// </summary>
        public ContentTransition()
        {

        }

        /// <summary>
        /// Transition duration
        /// </summary>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// Easing function of this transition
        /// </summary>
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        /// <summary>
        /// Create a storyboard for old content
        /// </summary>
        /// <param name="container">Container UIElement</param>
        /// <param name="oldContent">Old content UIElement</param>
        /// <param name="forward">Transition is forward</param>
        /// <returns></returns>
        protected abstract Storyboard CreateOldContentStoryboard(UIElement container, UIElement oldContent, bool forward);

        /// <summary>
        /// Create a storyboard for new content
        /// </summary>
        /// <param name="container">Container UIElement</param>
        /// <param name="newContent">New content UIElement</param>
        /// <param name="forward">Transition is forward</param>
        /// <returns></returns>
        protected abstract Storyboard CreateNewContentStoryboard(UIElement container, UIElement newContent, bool forward);

        /// <inheritdoc/>
        public async Task Run(FrameworkElement container, FrameworkElement? oldContent, FrameworkElement? newContent, bool forward, CancellationToken cancellationToken)
        {
            bool completed = false;
            Storyboard? newContentStoryboard = null;
            Storyboard? oldContentStoryboard = null;

            if (oldContent is not null)
            {
                oldContentStoryboard = CreateOldContentStoryboard(container, oldContent, forward);
            }
            if (newContent is not null)
            {
                newContentStoryboard = CreateNewContentStoryboard(container, newContent, forward);
            }

            var availableStoryboard = oldContentStoryboard ?? newContentStoryboard;

            if (availableStoryboard is null)
            {
                return;
            }

            availableStoryboard.Completed += (s, e) =>
            {
                completed = true;
            };

            newContentStoryboard?.Begin(newContent);
            oldContentStoryboard?.Begin(oldContent);

            while (true)
            {
                await Task.Delay(1);

                if (completed)
                {
                    break;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    newContentStoryboard?.Stop(newContent);
                    oldContentStoryboard?.Stop(oldContent);
                    break;
                }
            }
        }

        /// <summary>
        /// Coerce value of Duration property
        /// </summary>
        /// <param name="d"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected static object CoerceDuration(DependencyObject d, object value)
        {
            if (value is not Duration duration ||
                !duration.HasTimeSpan)
            {
                throw new ArgumentException();
            }

            return value;
        }

        /// <summary>
        /// The DependencyProperty of <see cref="Duration"/> property
        /// </summary>

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register(nameof(Duration), typeof(Duration), typeof(ContentTransition), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(150)), null, CoerceDuration));

        /// <summary>
        /// The DependencyProperty of <see cref="EasingFunction"/> property
        /// </summary>
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register(nameof(EasingFunction), typeof(IEasingFunction), typeof(ContentTransition), new PropertyMetadata(new CircleEase(){ EasingMode = EasingMode.EaseOut }));
    }
}
