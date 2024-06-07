using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Fade transition
    /// </summary>
    public class FadeTransition : ContentTransition
    {
        /// <inheritdoc/>
        protected override Freezable CreateInstanceCore() => new FadeTransition();

        /// <inheritdoc/>
        protected override Storyboard CreateNewContentStoryboard(UIElement container, UIElement newContent, bool forward)
        {
            if (newContent.RenderTransform is not TranslateTransform)
                newContent.RenderTransform = new TranslateTransform();

            DoubleAnimation translateAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                From = 0,
                To = 1,
            };

            Storyboard.SetTargetProperty(translateAnimation, new PropertyPath(nameof(FrameworkElement.Opacity)));

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

            DoubleAnimation translateAnimation = new()
            {
                EasingFunction = EasingFunction,
                Duration = Duration,
                To = 0,
            };

            Storyboard.SetTargetProperty(translateAnimation, new PropertyPath(nameof(FrameworkElement.Opacity)));

            return new Storyboard()
            {
                Duration = Duration,
                Children =
                {
                    translateAnimation
                }
            };
        }
    }
}
