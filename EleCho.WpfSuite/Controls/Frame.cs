using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EleCho.WpfSuite
{
    [TemplatePart(Name = "PART_ContentControl", Type = typeof(TransitioningContentControl))]
    public class Frame : System.Windows.Controls.Frame
    {
        static Frame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Frame), new FrameworkPropertyMetadata(typeof(Frame)));
            ContentProperty.OverrideMetadata(typeof(Frame), new FrameworkPropertyMetadata(null, OnFrameContentChanged));
        }

        private TransitioningContentControl? _contentControl;
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

        public IContentTransition Transition
        {
            get { return (IContentTransition)GetValue(TransitionProperty); }
            set { SetValue(TransitionProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentControl = GetTemplateChild("PART_ContentControl") as TransitioningContentControl;
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

        private static void OnFrameContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Frame frame)
            {
                frame.ApplyFrameContentChange(e.NewValue);
            }
        }


        public static readonly DependencyProperty CornerRadiusProperty =
            Border.CornerRadiusProperty.AddOwner(typeof(Frame));

        public static readonly DependencyProperty TransitionProperty =
            TransitioningContentControl.TransitionProperty.AddOwner(typeof(Frame));


    }
}
