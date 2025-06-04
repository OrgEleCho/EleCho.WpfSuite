using EleCho.WpfSuite.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EleCho.WpfSuite.Controls
{
    /// <inheritdoc/>
    public class ScrollViewer : System.Windows.Controls.ScrollViewer
    {
        static ScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollViewer), new FrameworkPropertyMetadata(typeof(ScrollViewer)));

            _propertyHandlesMouseWheelScrollingGetter = (GetBool)typeof(ScrollViewer)
                .GetProperty("HandlesMouseWheelScrolling", BindingFlags.Instance | BindingFlags.NonPublic)!
                .GetGetMethod(true)!
                .CreateDelegate(typeof(GetBool));
        }

        private delegate bool GetBool(ScrollViewer scrollViewer);
        private static readonly GetBool _propertyHandlesMouseWheelScrollingGetter;
        private static readonly IEasingFunction _scrollingAnimationEase = new CubicEase() { EasingMode = EasingMode.EaseOut };
        private const long _millisecondsBetweenTouchpadScrolling = 100;

        // 上次的滚动 delta, 不管是水平还是垂直, 都会保存下来
        // 用于判断是否是触摸板滚动

        private int _lastScrollDelta = 0;
        private int _lastVerticalScrollingDelta = 0;
        private int _lastHorizontalScrollingDelta = 0;
        private TimeSpan _lastScrollTime;
        private double _requestingSmoothVerticalOffsetTarget = double.NaN;
        private double _requestingSmoothHorizontalOffsetTarget = double.NaN;

        private FrameworkElement? _scrollContentPresenter;
        private SecondOrderDynamics? _verticalSecondOrderDynamics;
        private SecondOrderDynamics? _horizontalSecondOrderDynamics;

        private static readonly Stopwatch s_commonStopwatch = Stopwatch.StartNew();
        private static readonly HashSet<ScrollViewer> s_scrollViewersOnUI = new HashSet<ScrollViewer>();
        private static TimeSpan s_lastRenderingTime;

        /// <inheritdoc/>
        public ScrollViewer()
        {
            AddHandler(EleCho.WpfSuite.Input.Mouse.WheelEvent, (RoutedEventHandler)OnSuiteMouseWheel);
        }

        /// <inheritdoc/>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _scrollContentPresenter = GetTemplateChild("PART_ScrollContentPresenter") as FrameworkElement;
        }

        public bool CanScrollVertical
        {
            get
            {
                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    return scrollInfo.ExtentHeight - scrollInfo.ViewportHeight > 0;
                }

                return ExtentHeight > ViewportHeight;
            }
        }

        public bool CanScrollHorizontal
        {
            get
            {
                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    return scrollInfo.ExtentWidth - scrollInfo.ViewportWidth > 0;
                }

                return ExtentWidth - ViewportWidth > 0;
            }
        }

        public bool CanScrollUp
        {
            get
            {
                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    return scrollInfo.VerticalOffset > 0;
                }

                return VerticalOffset > 0;
            }
        }

        public bool CanScrollDown
        {
            get
            {
                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    return scrollInfo.VerticalOffset + scrollInfo.ViewportHeight < scrollInfo.ExtentHeight;
                }

                return VerticalOffset + ViewportHeight < ExtentHeight;
            }
        }

        public bool CanScrollLeft
        {
            get
            {
                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    return scrollInfo.HorizontalOffset > 0;
                }

                return HorizontalOffset > 0;
            }
        }

        public bool CanScrollRight
        {
            get
            {
                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    return scrollInfo.HorizontalOffset + scrollInfo.ViewportWidth < scrollInfo.ExtentWidth;
                }

                return HorizontalOffset + ViewportWidth < ExtentWidth;
            }
        }

        public bool AllowTogglePreferredScrollOrientationByShiftKey
        {
            get { return (bool)GetValue(AllowTogglePreferredScrollOrientationByShiftKeyProperty); }
            set { SetValue(AllowTogglePreferredScrollOrientationByShiftKeyProperty, value); }
        }

        public Orientation PreferredScrollOrientation
        {
            get { return (Orientation)GetValue(PreferredScrollOrientationProperty); }
            set { SetValue(PreferredScrollOrientationProperty, value); }
        }

        /// <inheritdoc/>
        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            if (VisualParent is not null)
            {
                s_scrollViewersOnUI.Add(this);
            }
            else
            {
                s_scrollViewersOnUI.Remove(this);
            }

            EnsureCompositionRenderingProcessStatus();
            base.OnVisualParentChanged(oldParent);
        }

        private static void EnsureCompositionRenderingProcessStatus()
        {
            if (s_scrollViewersOnUI.Count > 0)
            {
                s_lastRenderingTime = s_commonStopwatch.Elapsed;

                CompositionTarget.Rendering -= CompositionTargetRendering;
                CompositionTarget.Rendering += CompositionTargetRendering;
            }
            else
            {
                CompositionTarget.Rendering -= CompositionTargetRendering;
            }
        }

        private static void CompositionTargetRendering(object? sender, EventArgs e)
        {
            if (s_commonStopwatch is null)
            {
                return;
            }

            var nowRenderingTime = s_commonStopwatch.Elapsed;
            var elapsed = nowRenderingTime - s_lastRenderingTime;
            var elapsedSeconds = elapsed.TotalSeconds;
            if (elapsedSeconds == 0)
            {
                return;
            }

            s_lastRenderingTime = nowRenderingTime;

            foreach (var scrollViewer in s_scrollViewersOnUI)
            {
                var scrollInfo = scrollViewer.ScrollInfo;
                var verticalOffset = scrollInfo.VerticalOffset;
                var horizontalOffset = scrollInfo.HorizontalOffset;
                var verticalOffsetTarget = scrollViewer._requestingSmoothVerticalOffsetTarget;
                var horizontalOffsetTarget = scrollViewer._requestingSmoothHorizontalOffsetTarget;
                var dynamicsFrequency = 1 / scrollViewer.SmoothScrollingTime.TotalSeconds;

                if (!double.IsNaN(verticalOffsetTarget))
                {
                    scrollViewer._verticalSecondOrderDynamics ??= new SecondOrderDynamics(dynamicsFrequency, 1, 0, verticalOffset);
                    scrollViewer._verticalSecondOrderDynamics.F = dynamicsFrequency;

                    var animatedVerticalOffset = scrollViewer._verticalSecondOrderDynamics.Update(elapsedSeconds, verticalOffsetTarget, out var deltaValue);

                    if (Math.Abs(animatedVerticalOffset - verticalOffset) > 0.1)
                    {
                        scrollInfo.SetVerticalOffset(animatedVerticalOffset);
                    }

                    // stop 
                    if (Math.Abs(deltaValue) < 0.05)
                    {
                        scrollViewer._requestingSmoothVerticalOffsetTarget = double.NaN;
                        scrollViewer._verticalSecondOrderDynamics = null;
                    }
                }
                else
                {
                    scrollViewer._verticalSecondOrderDynamics = null;
                }

                if (!double.IsNaN(horizontalOffsetTarget))
                {
                    scrollViewer._horizontalSecondOrderDynamics ??= new SecondOrderDynamics(dynamicsFrequency, 1, 0, horizontalOffset);
                    scrollViewer._horizontalSecondOrderDynamics.F = dynamicsFrequency;

                    var animatedHorizontalOffset = scrollViewer._horizontalSecondOrderDynamics.Update(elapsedSeconds, horizontalOffsetTarget, out var deltaValue);

                    if (Math.Abs(animatedHorizontalOffset - horizontalOffset) > 0.1)
                    {
                        scrollInfo.SetHorizontalOffset(animatedHorizontalOffset);
                    }

                    // stop 
                    if (Math.Abs(deltaValue) < 0.05)
                    {
                        scrollViewer._requestingSmoothVerticalOffsetTarget = double.NaN;
                        scrollViewer._horizontalSecondOrderDynamics = null;
                    }
                }
                else
                {
                    scrollViewer._horizontalSecondOrderDynamics = null;
                }
            }
        }

        private bool CoreScrollWithWheelDelta(int deltaX, int deltaY)
        {
            if (!AlwaysHandleMouseWheelScrolling &&
                !_propertyHandlesMouseWheelScrollingGetter.Invoke(this))
            {
                return false;
            }

            var preferredScrollOrientation = PreferredScrollOrientation;
            if (AllowTogglePreferredScrollOrientationByShiftKey && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                preferredScrollOrientation = preferredScrollOrientation switch
                {
                    Orientation.Horizontal => Orientation.Vertical,
                    Orientation.Vertical => Orientation.Horizontal,
                    _ => Orientation.Vertical
                };
            }

            bool canScrollVertical = CanScrollVertical;
            bool canScrollHorizontal = CanScrollHorizontal;

            double scrollDelta = deltaY;
            if (deltaX != 0)
            {
                scrollDelta = -deltaX;
                preferredScrollOrientation = Orientation.Horizontal;

                if (!canScrollHorizontal)
                {
                    return false;
                }
            }

            var elapsed = s_commonStopwatch.Elapsed;
            var elapsedMillisecondsAfterLastScroll = (elapsed - _lastScrollTime).TotalMilliseconds;
            var isTouchpadScrolling =
                    scrollDelta % Mouse.MouseWheelDeltaForOneLine != 0 ||
                    (elapsedMillisecondsAfterLastScroll < _millisecondsBetweenTouchpadScrolling && _lastScrollDelta % Mouse.MouseWheelDeltaForOneLine != 0);

            bool canScrollHorizontalAndPreferHorizontal = canScrollHorizontal && preferredScrollOrientation == Orientation.Horizontal;

            if (isTouchpadScrolling)
            {
                // touchpad 应该滚动更慢一些, 所以这里预先除以一个合适的值
                scrollDelta /= 2;

                // 
                scrollDelta *= TouchpadScrollDeltaFactor;
            }
            else
            {
                scrollDelta *= MouseScrollDeltaFactor;
            }

            if (canScrollVertical && !canScrollHorizontalAndPreferHorizontal)
            {
                if (scrollDelta > 0 && !CanScrollUp)
                {
                    return false;
                }
                else if (scrollDelta < 0 && !CanScrollDown)
                {
                    return false;
                }

                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    // 考虑到 VirtualizingPanel 可能是虚拟的大小, 所以这里需要校正 Delta
                    scrollDelta *= scrollInfo.ViewportHeight / (_scrollContentPresenter?.ActualHeight ?? ActualHeight);
                }

                var sameDirectionAsLast = Math.Sign(deltaY) == Math.Sign(_lastVerticalScrollingDelta);
                var nowOffset = sameDirectionAsLast && !double.IsNaN(_requestingSmoothVerticalOffsetTarget) ? _requestingSmoothVerticalOffsetTarget : VerticalOffset;
                var newOffset = nowOffset - scrollDelta;

                if (newOffset < 0)
                    newOffset = 0;
                if (newOffset > ScrollableHeight)
                    newOffset = ScrollableHeight;

                if (!EnableSmoothScrolling || isTouchpadScrolling)
                {
                    _requestingSmoothVerticalOffsetTarget = double.NaN;
                    ScrollToVerticalOffset(newOffset);
                }
                else
                {
                    _requestingSmoothVerticalOffsetTarget = newOffset;
                }

                _lastVerticalScrollingDelta = deltaY;
            }
            else if (canScrollHorizontal)
            {
                if (scrollDelta > 0 && !CanScrollLeft)
                {
                    return false;
                }
                else if (scrollDelta < 0 && !CanScrollRight)
                {
                    return false;
                }

                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    // 考虑到 VirtualizingPanel 可能是虚拟的大小, 所以这里需要校正 Delta
                    scrollDelta *= scrollInfo.ViewportWidth / (_scrollContentPresenter?.ActualWidth ?? ActualWidth);
                }

                var sameDirectionAsLast = Math.Sign(deltaY) == Math.Sign(_lastHorizontalScrollingDelta);
                var nowOffset = sameDirectionAsLast && !double.IsNaN(_requestingSmoothHorizontalOffsetTarget) ? _requestingSmoothHorizontalOffsetTarget : HorizontalOffset;
                var newOffset = nowOffset - scrollDelta;

                if (newOffset < 0)
                    newOffset = 0;
                if (newOffset > ScrollableWidth)
                    newOffset = ScrollableWidth;

                if (!EnableSmoothScrolling || isTouchpadScrolling)
                {
                    _requestingSmoothHorizontalOffsetTarget = double.NaN;
                    ScrollToHorizontalOffset(newOffset);
                }
                else
                {
                    _requestingSmoothHorizontalOffsetTarget = newOffset;
                }

                _lastHorizontalScrollingDelta = deltaY;
            }
            else
            {
                // no scroll
                return false;
            }

            _lastScrollTime = elapsed;
            _lastScrollDelta = deltaY;

            return true;
        }

        /// <inheritdoc/>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (!ScrollWithWheelDelta)
            {
                base.OnMouseWheel(e);
            }
            else if (!e.Handled)
            {
                var handled = CoreScrollWithWheelDelta(0, e.Delta);
                e.Handled = handled;
            }
        }

        private void OnSuiteMouseWheel(object? sender, RoutedEventArgs e)
        {
            if (EleCho.WpfSuite.Input.Mouse.CurrentWheelDeltaX != 0)
            {
                var handled = CoreScrollWithWheelDelta(EleCho.WpfSuite.Input.Mouse.CurrentWheelDeltaX, 0);
                e.Handled = handled;
            }
        }

        /// <summary>
        /// Scroll with wheel delta instead of scrolling fixed number of lines
        /// </summary>
        public bool ScrollWithWheelDelta
        {
            get { return (bool)GetValue(ScrollWithWheelDeltaProperty); }
            set { SetValue(ScrollWithWheelDeltaProperty, value); }
        }

        /// <summary>
        /// Enable scrolling animation while using mouse <br/>
        /// You need to set ScrollWithWheelDelta to true to use this
        /// </summary>
        public bool EnableSmoothScrolling
        {
            get { return (bool)GetValue(EnableSmoothScrollingProperty); }
            set { SetValue(EnableSmoothScrollingProperty, value); }
        }

        /// <summary>
        /// Smooth scrolling time
        /// </summary>
        public TimeSpan SmoothScrollingTime
        {
            get { return (TimeSpan)GetValue(SmoothScrollingTimeProperty); }
            set { SetValue(SmoothScrollingTimeProperty, value); }
        }

        /// <summary>
        /// Delta value factor while mouse scrolling
        /// </summary>
        public double MouseScrollDeltaFactor
        {
            get { return (double)GetValue(MouseScrollDeltaFactorProperty); }
            set { SetValue(MouseScrollDeltaFactorProperty, value); }
        }

        /// <summary>
        /// Delta value factor while touchpad scrolling
        /// </summary>
        public double TouchpadScrollDeltaFactor
        {
            get { return (double)GetValue(TouchpadScrollDeltaFactorProperty); }
            set { SetValue(TouchpadScrollDeltaFactorProperty, value); }
        }

        /// <summary>
        /// Always handle mouse wheel scrolling. <br />
        /// (Especially in "TextBox")
        /// </summary>
        public bool AlwaysHandleMouseWheelScrolling
        {
            get { return (bool)GetValue(AlwaysHandleMouseWheelScrollingProperty); }
            set { SetValue(AlwaysHandleMouseWheelScrollingProperty, value); }
        }





        /// <summary>
        /// Get value of ScrollWithWheelDelta property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetScrollWithWheelDelta(DependencyObject obj)
        {
            return (bool)obj.GetValue(ScrollWithWheelDeltaProperty);
        }

        /// <summary>
        /// Set value of ScrollWithWheelDelta property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetScrollWithWheelDelta(DependencyObject obj, bool value)
        {
            obj.SetValue(ScrollWithWheelDeltaProperty, value);
        }

        /// <summary>
        /// Get value of EnableScrollingAnimation property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetEnableScrollingAnimation(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableSmoothScrollingProperty);
        }

        /// <summary>
        /// Set value of EnableScrollingAnimation property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetEnableScrollingAnimation(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableSmoothScrollingProperty, value);
        }


        /// <summary>
        /// Set value of AlwaysHandleMouseWheelScrolling property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetAlwaysHandleMouseWheelScrolling(DependencyObject obj)
        {
            return (bool)obj.GetValue(AlwaysHandleMouseWheelScrollingProperty);
        }

        /// <summary>
        /// Get value of AlwaysHandleMouseWheelScrolling property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetAlwaysHandleMouseWheelScrolling(DependencyObject obj, bool value)
        {
            obj.SetValue(AlwaysHandleMouseWheelScrollingProperty, value);
        }


        /// <summary>
        /// The DependencyProperty of <see cref="ScrollWithWheelDelta"/> property.
        /// </summary>
        public static readonly DependencyProperty ScrollWithWheelDeltaProperty =
            DependencyProperty.RegisterAttached(nameof(ScrollWithWheelDelta), typeof(bool), typeof(ScrollViewer),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// The DependencyProperty of <see cref="EnableSmoothScrolling"/> property.
        /// </summary>
        public static readonly DependencyProperty EnableSmoothScrollingProperty =
            DependencyProperty.RegisterAttached(nameof(EnableSmoothScrolling), typeof(bool), typeof(ScrollViewer),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// The DependencyProperty of <see cref="SmoothScrollingTime"/> property.
        /// </summary>
        public static readonly DependencyProperty SmoothScrollingTimeProperty =
            DependencyProperty.RegisterAttached(nameof(SmoothScrollingTime), typeof(TimeSpan), typeof(ScrollViewer),
                new FrameworkPropertyMetadata(TimeSpan.FromSeconds(0.2), FrameworkPropertyMetadataOptions.Inherits), ValidateSmoothScrollingTime);

        /// <summary>
        /// The DependencyProperty of <see cref="AlwaysHandleMouseWheelScrolling"/> property
        /// </summary>
        public static readonly DependencyProperty AlwaysHandleMouseWheelScrollingProperty =
            DependencyProperty.RegisterAttached(nameof(AlwaysHandleMouseWheelScrolling), typeof(bool), typeof(ScrollViewer), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// The DependencyProperty of <see cref="MouseScrollDeltaFactor"/> property
        /// </summary>
        public static readonly DependencyProperty MouseScrollDeltaFactorProperty =
            DependencyProperty.Register(nameof(MouseScrollDeltaFactor), typeof(double), typeof(ScrollViewer), new FrameworkPropertyMetadata(1.0));

        /// <summary>
        /// The DependencyProperty of <see cref="TouchpadScrollDeltaFactor"/> property
        /// </summary>
        public static readonly DependencyProperty TouchpadScrollDeltaFactorProperty =
            DependencyProperty.Register(nameof(TouchpadScrollDeltaFactor), typeof(double), typeof(ScrollViewer), new FrameworkPropertyMetadata(1.0));

        /// <summary>
        /// The DependencyProperty of <see cref="AllowTogglePreferredScrollOrientationByShiftKey"/> property
        /// </summary>
        public static readonly DependencyProperty AllowTogglePreferredScrollOrientationByShiftKeyProperty =
            DependencyProperty.Register(nameof(AllowTogglePreferredScrollOrientationByShiftKey), typeof(bool), typeof(ScrollViewer), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// The DependencyProperty of <see cref="PreferredScrollOrientationProperty"/> property
        /// </summary>
        public static readonly DependencyProperty PreferredScrollOrientationProperty =
            DependencyProperty.Register(nameof(PreferredScrollOrientation), typeof(Orientation), typeof(ScrollViewer), new FrameworkPropertyMetadata(Orientation.Vertical));

        private static bool ValidateSmoothScrollingTime(object value)
        {
            return value is TimeSpan timeSpan && timeSpan.TotalSeconds > 0;
        }
    }
}
