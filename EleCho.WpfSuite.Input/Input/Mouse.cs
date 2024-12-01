using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace EleCho.WpfSuite.Input
{
    /// <summary>
    /// Touchpad
    /// </summary>
    public class Mouse
    {
        private static int _currentScrollingDeltaX = 0;
        private static int _currentScrollingDeltaY = 0;

        /// <summary>
        /// 
        /// </summary>
        public static int CurrentWheelDeltaX => _currentScrollingDeltaX;

        /// <summary>
        /// 
        /// </summary>
        public static int CurrentWheelDeltaY => _currentScrollingDeltaY;

        static Mouse()
        {
            WheelEvent.AddOwner(typeof(UIElement));
            WheelEvent.AddOwner(typeof(UIElement3D));
        }

        private static HwndSource? GetWindowHwndSource(DependencyObject dependencyObject)
        {
            if (dependencyObject is Window window)
            {
                return PresentationSource.FromVisual(window) as HwndSource;
            }
            else if (dependencyObject is ToolTip tooltip)
            {
                return PresentationSource.FromVisual(tooltip) as HwndSource;
            }
            else if (dependencyObject is Popup popup)
            {
                if (popup.Child is null)
                    return null;

                return PresentationSource.FromVisual(popup.Child) as HwndSource;
            }
            else if (dependencyObject is Visual visual)
            {
                return PresentationSource.FromVisual(visual) as HwndSource;
            }

            return null;
        }

        private static void DoAfterHandleOk(DependencyObject dependencyObject, Action<DependencyObject, HwndSource> action)
        {
            if (GetWindowHwndSource(dependencyObject) is HwndSource hwndSource)
            {
                action.Invoke(dependencyObject, hwndSource);
                return;
            }

            if (dependencyObject is Window window)
            {
                var eventHandler = default(EventHandler);

                eventHandler = (s, e) =>
                {
                    if (GetWindowHwndSource(window) is HwndSource hwndSource)
                    {
                        action?.Invoke(dependencyObject, hwndSource);
                    }

                    window.SourceInitialized -= eventHandler;
                };

                window.SourceInitialized += eventHandler;
            }
            else if (dependencyObject is Popup popup)
            {
                var eventHandler = default(EventHandler);

                eventHandler = (s, e) =>
                {
                    if (GetWindowHwndSource(popup) is HwndSource hwndSource)
                    {
                        action?.Invoke(dependencyObject, hwndSource);
                    }

                    popup.Opened -= eventHandler;
                };

                popup.Opened += eventHandler;
            }
            else if (dependencyObject is ToolTip tooltip)
            {
                var eventHandler = default(RoutedEventHandler);

                eventHandler = (s, e) =>
                {
                    if (GetWindowHwndSource(tooltip) is HwndSource hwndSource)
                    {
                        action?.Invoke(dependencyObject, hwndSource);
                    }

                    tooltip.Opened -= eventHandler;
                };

                tooltip.Opened += eventHandler;
            }
            else
            {
                throw new NotSupportedException("Invalid dependency object");
            }
        }
        
        /// <summary>
        /// 取指针所在高位数值。
        /// </summary>
        private static short HIWORD(IntPtr ptr)
        {
            unchecked
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    var val64 = ptr.ToInt64();
                    return (short)((val64 >> 16) & 0xFFFF);
                }
                var val32 = ptr.ToInt32();
                return (short)((val32 >> 16) & 0xFFFF);
            }
        }

        public static bool GetScrollingSupport(DependencyObject obj)
        {
            return (bool)obj.GetValue(ScrollingSupportProperty);
        }

        public static void SetScrollingSupport(DependencyObject obj, bool value)
        {
            obj.SetValue(ScrollingSupportProperty, value);
        }

        public static readonly DependencyProperty ScrollingSupportProperty =
            DependencyProperty.RegisterAttached("ScrollingSupport", typeof(bool), typeof(Mouse), new PropertyMetadata(false, propertyChangedCallback: OnScrollingSupportChanged));

        public static readonly RoutedEvent WheelEvent = 
            EventManager.RegisterRoutedEvent("Wheel", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Mouse));

        private static void OnScrollingSupportChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool newValue = (bool)e.NewValue;

            DoAfterHandleOk(d, (d, hwndSource) =>
            {
                if (newValue)
                {
                    hwndSource.AddHook(WindowMessageHookForScrolling);
                }
                else
                {
                    hwndSource.RemoveHook(WindowMessageHookForScrolling);
                }
            });
        }

        private static nint WindowMessageHookForScrolling(nint hwnd, int msg, nint wParam, nint lParam, ref bool handled)
        {
            const int WM_MOUSEWHEEL = 0x020A;
            const int WM_MOUSEHWHEEL = 0x020E;

            switch (msg)
            {
                case WM_MOUSEWHEEL:
                {
                    int deltaY = HIWORD(wParam);
                    _currentScrollingDeltaX = 0;
                    _currentScrollingDeltaY = deltaY;
                    RaiseWheelEvent();
                    break;
                }
                case WM_MOUSEHWHEEL:
                {
                    int deltaX = HIWORD(wParam);
                    _currentScrollingDeltaX = deltaX;
                    _currentScrollingDeltaY = 0;
                    Debug.WriteLine($"Horizontal Scroll: {deltaX}");
                    RaiseWheelEvent();
                    break;
                }
            }

            return 0;
        }

        private static void RaiseWheelEvent()
        {
            if (System.Windows.Input.Mouse.DirectlyOver is UIElement element)
            {
                element.RaiseEvent(new RoutedEventArgs(WheelEvent));
            }
        }
    }
}
