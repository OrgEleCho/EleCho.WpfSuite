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

        private static readonly HashSet<IntPtr> _hookWindowHandles = new HashSet<IntPtr>();

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

        private static bool IsElementThatHasHwndSource(DependencyObject dependencyObject)
        {
            return
                dependencyObject is Window ||
                dependencyObject is Popup ||
                dependencyObject is ToolTip;
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

        private static void DoAfterElementLoaded(FrameworkElement element, Action action)
        {
            var eventHandler = default(RoutedEventHandler);

            eventHandler = (s, e) =>
            {
                action?.Invoke();
                element.Loaded -= eventHandler;
            };

            element.Loaded += eventHandler;
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
            else if (dependencyObject is FrameworkElement frameworkElement)
            {
                DoAfterElementLoaded(frameworkElement, () =>
                {
                    if (GetWindowHwndSource(frameworkElement) is HwndSource hwndSource)
                    {
                        action?.Invoke(dependencyObject, hwndSource);
                    }
                });
            }
            else
            {
                throw new InvalidOperationException("Invalid DependencyObject");
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

        public static bool GetWheelSupport(DependencyObject obj)
        {
            return (bool)obj.GetValue(WheelSupportProperty);
        }

        public static void SetWheelSupport(DependencyObject obj, bool value)
        {
            obj.SetValue(WheelSupportProperty, value);
        }

        public static readonly DependencyProperty WheelSupportProperty =
            DependencyProperty.RegisterAttached("WheelSupport", typeof(bool), typeof(Mouse), new FrameworkPropertyMetadata(false, flags: FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.OverridesInheritanceBehavior, propertyChangedCallback: OnWheelSupportChanged));

        public static readonly RoutedEvent WheelEvent = 
            EventManager.RegisterRoutedEvent("Wheel", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Mouse));

        private static void OnWheelSupportChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement)
            {
                return;
            }

            bool newValue = (bool)e.NewValue;

            if (newValue)
            {
                DoAfterHandleOk(d, (d, hwndSource) =>
                {
                    if (!_hookWindowHandles.Contains(hwndSource.Handle))
                    {
                        hwndSource.AddHook(WindowMessageHookForWheel);
                        _hookWindowHandles.Add(hwndSource.Handle);
                    }
                });
            }
            else
            {
                if (GetWindowHwndSource(d) is HwndSource hwndSource)
                {
                    hwndSource.RemoveHook(WindowMessageHookForWheel);
                    _hookWindowHandles.Remove(hwndSource.Handle);
                }
            }
        }

        private static nint WindowMessageHookForWheel(nint hwnd, int msg, nint wParam, nint lParam, ref bool handled)
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

        private static FrameworkElement? FindFrameworkElement(DependencyObject dependencyObject)
        {
            while (true)
            {
                if (dependencyObject is FrameworkElement frameworkElement)
                {
                    return frameworkElement;
                }

                var parent = VisualTreeHelper.GetParent(dependencyObject);

                if (parent is null)
                {
                    return null;
                }

                dependencyObject = parent;
            }
        }

        private static void RaiseWheelEvent()
        {
            if (System.Windows.Input.Mouse.DirectlyOver is UIElement element &&
                FindFrameworkElement(element) is FrameworkElement frameworkElement &&
                GetWheelSupport(frameworkElement))
            {
                element.RaiseEvent(new RoutedEventArgs(WheelEvent));
            }
        }
    }
}
