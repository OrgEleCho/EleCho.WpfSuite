using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EleCho.WpfSuite.Controls
{
    /// <summary>
    /// Frame is a content control that supports navigation.
    /// </summary>
    [TemplatePart(Name = "PART_ContentControl", Type = typeof(ContentControl))]
    public class Frame : System.Windows.Controls.Frame
    {
        static Frame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Frame), new FrameworkPropertyMetadata(typeof(Frame)));
            ContentProperty.OverrideMetadata(typeof(Frame), new FrameworkPropertyMetadata(null, new CoerceValueCallback(CoerceContent)));
        }

        private ContentControl? _contentControl;
        private object? _pendingNewContent;
        private int _lastBackStackSize;



        /// <summary>
        /// The CornerRadius property allows users to control the roundness of the corners independently by
        /// setting a radius value for each corner.  Radius values that are too large are scaled so that they
        /// smoothly blend from corner to corner.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// Transition of content switching
        /// </summary>
        public IContentTransition Transition
        {
            get { return (IContentTransition)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentControl = GetTemplateChild("PART_ContentControl") as ContentControl;
            if (_contentControl is not null && 
                _pendingNewContent is not null)
            {
                ApplyFrameContentChange(_pendingNewContent);
            }
        }

        private int GetBackStackSize()
        {
            if (BackStack is null)
                return 0;

            int count = 0;
            foreach (object _ in BackStack)
            {
                count++;
            }

            return count;
        }

        private void ApplyFrameContentChange(object? content)
        {
            if (_contentControl is null)
            {
                _pendingNewContent = content;
                return;
            }

            var currentBackStackSize = GetBackStackSize();
            var forward = currentBackStackSize >= _lastBackStackSize;

            _pendingNewContent = null;
            _contentControl.SetContent(content, forward);
            _lastBackStackSize = currentBackStackSize;
        }

        private static object CoerceContent(DependencyObject d, object value)
        {
            var frame = (Frame)d;

            if (frame.NavigationService.Content == value)
            {
                frame.ApplyFrameContentChange(value);
                return value;
            }

            frame.Navigate(value);

            return DependencyProperty.UnsetValue;
        }


        /// <summary>
        /// The DependencyProperty of <see cref="CornerRadius"/> property
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(Frame));

        /// <summary>
        /// The DependencyProperty of <see cref="Transition"/> property
        /// </summary>
        public static readonly DependencyProperty TransitionProperty =
            ContentControl.TransitionProperty.AddOwner(typeof(Frame));


    }
}
