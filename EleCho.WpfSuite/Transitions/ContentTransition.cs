using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    public abstract class ContentTransition : Freezable, IContentTransition
    {
        public ContentTransition()
        {

        }

        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        protected abstract Storyboard CreateOldContentStoryboard(UIElement container, UIElement oldContent, bool forward);
        protected abstract Storyboard CreateNewContentStoryboard(UIElement container, UIElement newContent, bool forward);


        public async Task Run(FrameworkElement container, FrameworkElement? oldContent, FrameworkElement? newContent, bool forward, CancellationToken cancellationToken)
        {
            bool completed = false;
            Storyboard? newContentStoryboard = null;
            Storyboard? oldContentStorybaord = null;

            if (oldContent is not null)
            {
                oldContentStorybaord = CreateOldContentStoryboard(container, oldContent, forward);
            }
            if (newContent is not null)
            {
                newContentStoryboard = CreateNewContentStoryboard(container, newContent, forward);
            }

            var availableStoryboard = oldContentStorybaord ?? newContentStoryboard;

            if (availableStoryboard is null)
            {
                return;
            }

            availableStoryboard.Completed += (s, e) =>
            {
                completed = true;
            };

            newContentStoryboard?.Begin(newContent);
            oldContentStorybaord?.Begin(oldContent);

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
                    oldContentStorybaord?.Stop(oldContent);
                    break;
                }
            }
        }


        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register(nameof(Duration), typeof(Duration), typeof(ContentTransition), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(150))));

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register(nameof(EasingFunction), typeof(IEasingFunction), typeof(ContentTransition), new PropertyMetadata(new CircleEase(){ EasingMode = EasingMode.EaseOut }));
    }
}
