using EleCho.WpfSuite.Internal;
using EleCho.WpfSuite.Media.Animation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private const long _millisecondsBetweenTouchpadScrolling = 100;

        // 上次的滚动 delta, 不管是水平还是垂直, 都会保存下来
        // 用于判断是否是触摸板滚动

        private int _lastScrollDelta = 0;
        private int _lastVerticalScrollingDelta = 0;
        private int _lastHorizontalScrollingDelta = 0;
        private TimeSpan _lastScrollTime;

        private FrameworkElement? _scrollContentPresenter;
        private readonly ValueAnimator<double> _verticalScrollOffsetAnimator;
        private readonly ValueAnimator<double> _horizontalScrollOffsetAnimator;

        private static readonly Stopwatch s_commonStopwatch = Stopwatch.StartNew();
        private double? _lastVerticalOffsetBySmoothScrolling = null;
        private double? _lastHorizontalOffsetBySmoothScrolling = null;

        /// <inheritdoc/>
        public ScrollViewer()
        {
            AddHandler(EleCho.WpfSuite.Input.Mouse.WheelEvent, (RoutedEventHandler)OnSuiteMouseWheel);

            _verticalScrollOffsetAnimator = new ValueAnimator<double>();
            _horizontalScrollOffsetAnimator = new ValueAnimator<double>();

            var frequency = 1 / SmoothScrollingTime.TotalSeconds;
            _verticalScrollOffsetAnimator.Frequency = frequency;
            _horizontalScrollOffsetAnimator.Frequency = frequency;

            _verticalScrollOffsetAnimator.AnimatedValueChanged += VerticalScrollOffsetAnimator_AnimatedValueChanged;
            _horizontalScrollOffsetAnimator.AnimatedValueChanged += HorizontalScrollOffsetAnimator_AnimatedValueChanged;
        }

        private void HorizontalScrollOffsetAnimator_AnimatedValueChanged(object? sender, EventArgs e)
        {
            if (sender is ValueAnimator<double> valueAnimator)
            {
                var nowOffset = HorizontalOffset;
                if (_lastHorizontalOffsetBySmoothScrolling is not null &&
                    _lastHorizontalOffsetBySmoothScrolling.Value != nowOffset)
                {
                    _lastHorizontalOffsetBySmoothScrolling = null;
                    _horizontalScrollOffsetAnimator.StopImmediately(nowOffset);
                    return;
                }

                var newOffset = valueAnimator.AnimatedValue;
                ScrollToHorizontalOffset(newOffset);
                _lastHorizontalOffsetBySmoothScrolling = newOffset;
            }
        }

        private void VerticalScrollOffsetAnimator_AnimatedValueChanged(object? sender, EventArgs e)
        {
            if (sender is ValueAnimator<double> valueAnimator)
            {
                var nowOffset = VerticalOffset;
                if (_lastVerticalOffsetBySmoothScrolling is not null &&
                    _lastVerticalOffsetBySmoothScrolling.Value != nowOffset)
                {
                    _lastVerticalOffsetBySmoothScrolling = null;
                    _verticalScrollOffsetAnimator.StopImmediately(nowOffset);
                    return;
                }

                var newOffset = valueAnimator.AnimatedValue;
                ScrollToVerticalOffset(newOffset);
                _lastVerticalOffsetBySmoothScrolling = newOffset;
            }
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
                    return scrollInfo.VerticalOffset > 0.5;
                }

                return VerticalOffset > 0.5;
            }
        }

        public bool CanScrollDown
        {
            get
            {
                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    return scrollInfo.VerticalOffset + scrollInfo.ViewportHeight < scrollInfo.ExtentHeight - 0.5;
                }

                return VerticalOffset + ViewportHeight < ExtentHeight - 0.5;
            }
        }

        public bool CanScrollLeft
        {
            get
            {
                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    return scrollInfo.HorizontalOffset > 0.5;
                }

                return HorizontalOffset > 0.5;
            }
        }

        public bool CanScrollRight
        {
            get
            {
                if (ScrollInfo is IScrollInfo scrollInfo)
                {
                    return scrollInfo.HorizontalOffset + scrollInfo.ViewportWidth < scrollInfo.ExtentWidth - 0.5;
                }

                return HorizontalOffset + ViewportWidth < ExtentWidth - 0.5;
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

            var gonnaUseSmoothScrolling = EnableSmoothScrolling && !isTouchpadScrolling;

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
                var nowOffset = VerticalOffset;
                if (gonnaUseSmoothScrolling)
                {
                    if (sameDirectionAsLast && _verticalScrollOffsetAnimator.IsRunning)
                    {
                        nowOffset = _verticalScrollOffsetAnimator.Value;
                    }
                    else
                    {
                        _verticalScrollOffsetAnimator.UpdateValueDirectly(nowOffset);
                    }
                }

                var newOffset = nowOffset - scrollDelta;

                if (newOffset < 0)
                    newOffset = 0;
                if (newOffset > ScrollableHeight)
                    newOffset = ScrollableHeight;

                _lastVerticalOffsetBySmoothScrolling = null;
                if (!gonnaUseSmoothScrolling)
                {
                    // ensure animation stopped
                    _verticalScrollOffsetAnimator.StopImmediately();
                    ScrollToVerticalOffset(newOffset);
                }
                else
                {
                    _verticalScrollOffsetAnimator.Value = newOffset;
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
                var nowOffset = HorizontalOffset;
                if (gonnaUseSmoothScrolling)
                {
                    if (sameDirectionAsLast && _horizontalScrollOffsetAnimator.IsRunning)
                    {
                        nowOffset = _horizontalScrollOffsetAnimator.Value;
                    }
                    else
                    {
                        _horizontalScrollOffsetAnimator.UpdateValueDirectly(nowOffset);
                    }
                }

                var newOffset = nowOffset - scrollDelta;

                if (newOffset < 0)
                    newOffset = 0;
                if (newOffset > ScrollableWidth)
                    newOffset = ScrollableWidth;

                _lastHorizontalOffsetBySmoothScrolling = null;
                if (!gonnaUseSmoothScrolling)
                {
                    _horizontalScrollOffsetAnimator.StopImmediately();
                    ScrollToHorizontalOffset(newOffset);
                }
                else
                {
                    _horizontalScrollOffsetAnimator.Value = newOffset;
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
                new FrameworkPropertyMetadata(TimeSpan.FromSeconds(0.3), FrameworkPropertyMetadataOptions.Inherits, propertyChangedCallback: OnSmoothScrollingTimeChanged), ValidateSmoothScrollingTime);

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

        private static void OnSmoothScrollingTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ScrollViewer scrollViewer ||
                e.NewValue is not TimeSpan smoothTime)
            {
                return;
            }

            var frequency = 1 / smoothTime.TotalSeconds;
            scrollViewer._verticalScrollOffsetAnimator.Frequency = frequency;
            scrollViewer._horizontalScrollOffsetAnimator.Frequency = frequency;
        }
    }
}
