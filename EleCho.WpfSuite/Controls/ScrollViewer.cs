using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EleCho.WpfSuite
{
    public class ScrollViewer : System.Windows.Controls.ScrollViewer
    {
        static ScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollViewer), new FrameworkPropertyMetadata(typeof(ScrollViewer)));

#if NETCOREAPP
            _propertyHandlesMouseWheelScrollingGetter = typeof(ScrollViewer)
                .GetProperty("HandlesMouseWheelScrolling", BindingFlags.Instance | BindingFlags.NonPublic)!
                .GetGetMethod(true)!
                .CreateDelegate<GetBool>();
#else
            _propertyHandlesMouseWheelScrollingGetter = (GetBool)typeof(ScrollViewer)
                .GetProperty("HandlesMouseWheelScrolling", BindingFlags.Instance | BindingFlags.NonPublic)!
                .GetGetMethod(true)!
                .CreateDelegate(typeof(GetBool));
#endif
        }

        private delegate bool GetBool(ScrollViewer scrollViewer);
        private static readonly GetBool _propertyHandlesMouseWheelScrollingGetter;
        private static readonly IEasingFunction _scrollingAnimationEase = new CubicEase(){ EasingMode = EasingMode.EaseOut };

        private bool _animationRunning = false;

        private void CoreScrollWithWheelDelta(MouseWheelEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }

            var handlesMouseWheelScrolling = _propertyHandlesMouseWheelScrollingGetter.Invoke(this);
            if (!handlesMouseWheelScrolling)
            {
                return;
            }

            bool vertical = ExtentHeight > 0;
            bool horizontal = ExtentWidth > 0;

            if (vertical)
            {
                var nowOffset = _animationRunning ? VerticalOffsetTarget : VerticalOffset;
                var newOffset = nowOffset - e.Delta;

                if (newOffset < 0)
                    newOffset = 0;
                if (newOffset > ScrollableHeight)
                    newOffset = ScrollableHeight;

                SetValue(VerticalOffsetTargetPropertyKey, newOffset);
                BeginAnimation(ScrollViewerUtils.VerticalOffsetProperty, null);

                if (!EnableScrollingAnimation || Math.Abs(e.Delta) < Mouse.MouseWheelDeltaForOneLine)
                {
                    ScrollToVerticalOffset(newOffset);
                }
                else
                {
                    var absDiff = Math.Abs(newOffset - nowOffset);
                    var duration = ScrollingAnimationDuration;
                    if (absDiff < Mouse.MouseWheelDeltaForOneLine)
                    {
                        duration = new Duration(TimeSpan.FromTicks((long)(duration.TimeSpan.Ticks * absDiff / Mouse.MouseWheelDeltaForOneLine)));
                    }

                    DoubleAnimation doubleAnimation = new DoubleAnimation()
                    {
                        EasingFunction = _scrollingAnimationEase,
                        Duration = duration,
                        From = VerticalOffset,
                        To = newOffset,
                    };

                    doubleAnimation.Completed += DoubleAnimation_Completed;

                    _animationRunning = true;
                    BeginAnimation(ScrollViewerUtils.VerticalOffsetProperty, doubleAnimation, HandoffBehavior.SnapshotAndReplace);
                }
            }
            else if (horizontal)
            {
                var nowOffset = _animationRunning ? HorizontalOffsetTarget : HorizontalOffset;
                var newOffset = nowOffset - e.Delta;

                if (newOffset < 0)
                    newOffset = 0;
                if (newOffset > ScrollableWidth)
                    newOffset = ScrollableWidth;

                SetValue(HorizontalOffsetTargetPropertyKey, newOffset);
                BeginAnimation(ScrollViewerUtils.HorizontalOffsetProperty, null);

                if (!EnableScrollingAnimation || Math.Abs(e.Delta) < Mouse.MouseWheelDeltaForOneLine)
                {
                    ScrollToHorizontalOffset(newOffset);
                }
                else
                {
                    var absDiff = Math.Abs(newOffset - nowOffset);
                    var duration = ScrollingAnimationDuration;
                    if (absDiff < Mouse.MouseWheelDeltaForOneLine)
                    {
                        duration = new Duration(TimeSpan.FromTicks((long)(duration.TimeSpan.Ticks * absDiff / Mouse.MouseWheelDeltaForOneLine)));
                    }

                    DoubleAnimation doubleAnimation = new DoubleAnimation()
                    {
                        EasingFunction = _scrollingAnimationEase,
                        Duration = duration,
                        From = HorizontalOffset,
                        To = newOffset,
                    };

                    doubleAnimation.Completed += DoubleAnimation_Completed;

                    _animationRunning = true;
                    BeginAnimation(ScrollViewerUtils.HorizontalOffsetProperty, doubleAnimation, HandoffBehavior.SnapshotAndReplace);
                }
            }

            e.Handled = true;
        }

        private void DoubleAnimation_Completed(object? sender, EventArgs e)
        {
            _animationRunning = false;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (!ScrollWithWheelDelta)
            {
                base.OnMouseWheel(e);
            }
            else
            {
                Debug.WriteLine(e.Delta);

                CoreScrollWithWheelDelta(e);
            }
        }

        public bool ScrollWithWheelDelta
        {
            get { return (bool)GetValue(ScrollWithWheelDeltaProperty); }
            set { SetValue(ScrollWithWheelDeltaProperty, value); }
        }

        public double HorizontalOffsetTarget
        {
            get { return (double)GetValue(HorizontalOffsetTargetProperty); }
        }

        public double VerticalOffsetTarget
        {
            get { return (double)GetValue(VerticalOffsetTargetProperty); }
        }

        public bool EnableScrollingAnimation
        {
            get { return (bool)GetValue(EnableScrollingAnimationProperty); }
            set { SetValue(EnableScrollingAnimationProperty, value); }
        }

        public Duration ScrollingAnimationDuration
        {
            get { return (Duration)GetValue(ScrollingAnimationDurationProperty); }
            set { SetValue(ScrollingAnimationDurationProperty, value); }
        }



        public static readonly DependencyPropertyKey HorizontalOffsetTargetPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(HorizontalOffsetTarget), typeof(double), typeof(ScrollViewer), new PropertyMetadata(0.0));

        public static readonly DependencyPropertyKey VerticalOffsetTargetPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(VerticalOffsetTarget), typeof(double), typeof(ScrollViewer), new PropertyMetadata(0.0));

        public static readonly DependencyProperty HorizontalOffsetTargetProperty = 
            HorizontalOffsetTargetPropertyKey.DependencyProperty;

        public static readonly DependencyProperty VerticalOffsetTargetProperty =
            VerticalOffsetTargetPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ScrollWithWheelDeltaProperty =
            DependencyProperty.Register(nameof(ScrollWithWheelDelta), typeof(bool), typeof(ScrollViewer), new PropertyMetadata(true));

        public static readonly DependencyProperty EnableScrollingAnimationProperty =
            DependencyProperty.Register(nameof(EnableScrollingAnimation), typeof(bool), typeof(ScrollViewer), new PropertyMetadata(true));

        public static readonly DependencyProperty ScrollingAnimationDurationProperty =
            DependencyProperty.Register(nameof(ScrollingAnimationDuration), typeof(Duration), typeof(ScrollViewer), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(300))), ValidateScrollingAnimationDuration);

        private static bool ValidateScrollingAnimationDuration(object value)
            => value is Duration duration && duration.HasTimeSpan;
    }
}
