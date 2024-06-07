using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;
using System.Xml.Linq;
using static EleCho.WpfSuite.WindowOption.NativeDefinition;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// Window options
    /// </summary>
    public partial class WindowOption
    {
        const int BackdropTypeNone = 1;
        const int BackdropTypeMica = 2;
        const int BackdropTypeAcrylic = 3;

        static readonly Version s_versionWindows10_1809 = new Version(10, 0, 17763);
        static readonly Version s_versionWindows10 = new Version(10, 0);

        /// <summary>
        /// DWM 支持 Corner, BorderColor, CaptionColor, TextColor
        /// </summary>
        static readonly Version s_versionWindows11_22000 = new Version(10, 0, 22000);

        /// <summary>
        /// DWM 支持暗色模式与 Backdrop 属性
        /// </summary>
        static readonly Version s_versionWindows11_22621 = new Version(10, 0, 22621);

        static readonly Version s_versionCurrentWindows = Environment.OSVersion.Version;

        /// <summary>
        /// Check whether the current platform can set backdrop property
        /// </summary>
        public static bool CanSetBackdrop => s_versionCurrentWindows >= s_versionWindows11_22621;

        /// <summary>
        /// Check whether the current platform can set accent properties
        /// </summary>
        public static bool CanSetAccent => s_versionCurrentWindows >= s_versionWindows10_1809;



        /// <summary>
        /// Get value of Backdrop property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static WindowBackdrop GetBackdrop(DependencyObject obj)
        {
            return (WindowBackdrop)obj.GetValue(BackdropProperty);
        }

        /// <summary>
        /// Set value of Backdrop property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetBackdrop(DependencyObject obj, WindowBackdrop value)
        {
            obj.SetValue(BackdropProperty, value);
        }


        /// <summary>
        /// Get value of Corner property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static WindowCorner GetCorner(DependencyObject obj)
        {
            return (WindowCorner)obj.GetValue(CornerProperty);
        }

        /// <summary>
        /// Set value of Corner property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetCorner(DependencyObject obj, WindowCorner value)
        {
            obj.SetValue(CornerProperty, value);
        }


        /// <summary>
        /// Get value of CaptionColor property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static WindowOptionColor GetCaptionColor(DependencyObject obj)
        {
            return (WindowOptionColor)obj.GetValue(CaptionColorProperty);
        }

        /// <summary>
        /// Set value of CaptionColor property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetCaptionColor(DependencyObject obj, WindowOptionColor value)
        {
            obj.SetValue(CaptionColorProperty, value);
        }


        /// <summary>
        /// Get value of TextColor property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static WindowOptionColor GetTextColor(DependencyObject obj)
        {
            return (WindowOptionColor)obj.GetValue(TextColorProperty);
        }

        /// <summary>
        /// Set value of TextColor property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetTextColor(DependencyObject obj, WindowOptionColor value)
        {
            obj.SetValue(TextColorProperty, value);
        }


        /// <summary>
        /// Get value of BorderColor property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static WindowOptionColor GetBorderColor(DependencyObject obj)
        {
            return (WindowOptionColor)obj.GetValue(BorderColorProperty);
        }

        /// <summary>
        /// Set value of BorderColor property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetBorderColor(DependencyObject obj, WindowOptionColor value)
        {
            obj.SetValue(BorderColorProperty, value);
        }


        /// <summary>
        /// Get value of IsDarkMode property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetIsDarkMode(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDarkModeProperty);
        }

        /// <summary>
        /// Set value of IsDarkMode property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsDarkMode(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDarkModeProperty, value);
        }


        /// <summary>
        /// Get value of AccentState property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static WindowAccentState GetAccentState(DependencyObject obj)
        {
            return (WindowAccentState)obj.GetValue(AccentStateProperty);
        }

        /// <summary>
        /// Set value of AccentState property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetAccentState(DependencyObject obj, WindowAccentState value)
        {
            obj.SetValue(AccentStateProperty, value);
        }


        /// <summary>
        /// Get value of AccentBorder property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetAccentBorder(DependencyObject obj)
        {
            return (bool)obj.GetValue(AccentBorderProperty);
        }

        /// <summary>
        /// Set value of AccentBorder property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetAccentBorder(DependencyObject obj, bool value)
        {
            obj.SetValue(AccentBorderProperty, value);
        }


        /// <summary>
        /// Get value of AccentGradientColor property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static Color GetAccentGradientColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(AccentGradientColorProperty);
        }

        /// <summary>
        /// Set value of AccentGradientColor property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetAccentGradientColor(DependencyObject obj, Color value)
        {
            obj.SetValue(AccentGradientColorProperty, value);
        }


        /// <summary>
        /// The DependencyProperty of Backdrop property
        /// </summary>
        public static readonly DependencyProperty BackdropProperty =
            DependencyProperty.RegisterAttached("Backdrop", typeof(WindowBackdrop), typeof(WindowOption), new FrameworkPropertyMetadata(WindowBackdrop.Auto, OnBackdropChanged));

        /// <summary>
        /// The DependencyProperty of Corner property
        /// </summary>
        public static readonly DependencyProperty CornerProperty =
            DependencyProperty.RegisterAttached("Corner", typeof(WindowCorner), typeof(WindowOption), new PropertyMetadata(WindowCorner.Default, OnCornerChanged));

        /// <summary>
        /// The DependencyProperty of CaptionColor property
        /// </summary>
        public static readonly DependencyProperty CaptionColorProperty =
            DependencyProperty.RegisterAttached("CaptionColor", typeof(WindowOptionColor), typeof(WindowOption), new PropertyMetadata(WindowOptionColor.Default, OnCaptionColorChanged));

        /// <summary>
        /// The DependencyProperty of TextColor property
        /// </summary>
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.RegisterAttached("TextColor", typeof(WindowOptionColor), typeof(WindowOption), new PropertyMetadata(WindowOptionColor.Default, OnTextColorChanged));

        /// <summary>
        /// The DependencyProperty of BorderColor property
        /// </summary>
        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.RegisterAttached("BorderColor", typeof(WindowOptionColor), typeof(WindowOption), new PropertyMetadata(WindowOptionColor.Default, OnBorderColorChanged));

        /// <summary>
        /// The DependencyProperty of IsDarkMode property
        /// </summary>
        public static readonly DependencyProperty IsDarkModeProperty =
            DependencyProperty.RegisterAttached("IsDarkMode", typeof(bool), typeof(WindowOption), new FrameworkPropertyMetadata(false, OnIsDarkModeChanged));

        /// <summary>
        /// The DependencyProperty of AccentState property
        /// </summary>
        public static readonly DependencyProperty AccentStateProperty =
            DependencyProperty.RegisterAttached("AccentState", typeof(WindowAccentState), typeof(WindowOption), new FrameworkPropertyMetadata(WindowAccentState.None, OnAccentChanged));

        /// <summary>
        /// The DependencyProperty of AccentBorder property
        /// </summary>
        public static readonly DependencyProperty AccentBorderProperty =
            DependencyProperty.RegisterAttached("AccentBorder", typeof(bool), typeof(WindowOption), new FrameworkPropertyMetadata(true, OnAccentChanged));

        /// <summary>
        /// The DependencyProperty of AccentGradientColor property
        /// </summary>
        public static readonly DependencyProperty AccentGradientColorProperty =
            DependencyProperty.RegisterAttached("AccentGradientColor", typeof(Color), typeof(WindowOption), new FrameworkPropertyMetadata(Color.FromScRgb(0.25f, 1, 1, 1), OnAccentChanged));


        #region DependencyProperty Callbacks

        private static void OnBackdropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // check
            if (GetAccentState(d) is not WindowAccentState.None &&
                GetBackdrop(d) is not WindowBackdrop.Auto &&
                GetBackdrop(d) is not WindowBackdrop.None)
            {
                throw new InvalidOperationException("Backdrop and AccentState can't both be set");
            }

            if (d is Window window)
            {
                if (GetWindowHwndSource(d) is HwndSource hwndSource)
                {
                    ApplyBackdrop(window, hwndSource, GetBackdrop(d));
                }
                else
                {
                    DoAfterHandleOk(d, (d, hwndSource) =>
                    {
                        ApplyBackdrop(window, hwndSource, GetBackdrop(d));
                    });
                }
            }
            else if (d is Popup popup)
            {
                popup.Opened -= EventHandlerApplyBackdrop;
                popup.Opened += EventHandlerApplyBackdrop;
            }
            else if (d is FrameworkElement element)
            {
                element.Loaded -= EventHandlerApplyBackdrop;
                element.Loaded += EventHandlerApplyBackdrop;
            }
        }

        private static void OnCornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                if (GetWindowHwndSource(d) is HwndSource hwndSource)
                {
                    ApplyCorner(window, hwndSource, GetCorner(d));
                }
                else
                {
                    DoAfterHandleOk(d, (d, hwndSource) =>
                    {
                        ApplyCorner(window, hwndSource, GetCorner(d));
                    });
                }
            }
            else if (d is Popup popup)
            {
                popup.Opened -= EventHandlerApplyCorner;
                popup.Opened += EventHandlerApplyCorner;
            }
            else if (d is FrameworkElement element)
            {
                element.Loaded -= EventHandlerApplyCorner;
                element.Loaded += EventHandlerApplyCorner;
            }
        }

        private static void OnCaptionColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                if (GetWindowHwndSource(d) is HwndSource hwndSource)
                {
                    ApplyCaptionColor(window, hwndSource, GetCaptionColor(d));
                }
                else
                {
                    DoAfterHandleOk(d, (d, hwndSource) =>
                    {
                        ApplyCaptionColor(window, hwndSource, GetCaptionColor(d));
                    });
                }
            }
            else if (d is Popup popup)
            {
                popup.Opened -= EventHandlerApplyCaptionColor;
                popup.Opened += EventHandlerApplyCaptionColor;
            }
            else if (d is FrameworkElement element)
            {
                element.Loaded -= EventHandlerApplyCaptionColor;
                element.Loaded += EventHandlerApplyCaptionColor;
            }
        }

        private static void OnTextColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                if (GetWindowHwndSource(d) is HwndSource hwndSource)
                {
                    ApplyTextColor(window, hwndSource, GetTextColor(d));
                }
                else
                {
                    DoAfterHandleOk(d, (d, hwndSource) =>
                    {
                        ApplyTextColor(window, hwndSource, GetTextColor(d));
                    });
                }
            }
            else if (d is Popup popup)
            {
                popup.Opened -= EventHandlerApplyTextColor;
                popup.Opened += EventHandlerApplyTextColor;
            }
            else if (d is FrameworkElement element)
            {
                element.Loaded -= EventHandlerApplyTextColor;
                element.Loaded += EventHandlerApplyTextColor;
            }
        }

        private static void OnBorderColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                if (GetWindowHwndSource(d) is HwndSource hwndSource)
                {
                    ApplyBorderColor(window, hwndSource, GetBorderColor(d));
                }
                else
                {
                    DoAfterHandleOk(d, (d, hwndSource) =>
                    {
                        ApplyBorderColor(window, hwndSource, GetBorderColor(d));
                    });
                }
            }
            else if (d is Popup popup)
            {
                popup.Opened -= EventHandlerApplyBorderColor;
                popup.Opened += EventHandlerApplyBorderColor;
            }
            else if (d is FrameworkElement element)
            {
                element.Loaded -= EventHandlerApplyBorderColor;
                element.Loaded += EventHandlerApplyBorderColor;
            }
        }

        private static void OnIsDarkModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                if (GetWindowHwndSource(d) is HwndSource hwndSource)
                {
                    ApplyDarkMode(window, hwndSource, GetIsDarkMode(d));
                }
                else
                {
                    DoAfterHandleOk(d, (d, hwndSource) =>
                    {
                        ApplyDarkMode(window, hwndSource, GetIsDarkMode(d));
                    });
                }
            }
            else if (d is Popup popup)
            {
                popup.Opened -= EventHandlerApplyDarkMode;
                popup.Opened += EventHandlerApplyDarkMode;
            }
            else if (d is FrameworkElement element)
            {
                element.Loaded -= EventHandlerApplyDarkMode;
                element.Loaded += EventHandlerApplyDarkMode;
            }
        }

        private static void OnAccentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // check
            if (GetAccentState(d) is not WindowAccentState.None &&
                GetBackdrop(d) is not WindowBackdrop.Auto &&
                GetBackdrop(d) is not WindowBackdrop.None)
            {
                throw new InvalidOperationException("Backdrop and AccentState can't both be set");
            }

            if (d is Window window)
            {
                if (GetWindowHwndSource(d) is HwndSource hwndSource)
                {
                    ApplyAccent(window, hwndSource, GetAccentState(d), GetAccentGradientColor(d), GetAccentBorder(d));
                }
                else if (e.Property == AccentStateProperty)
                {
                    DoAfterHandleOk(d, (d, hwndSource) =>
                    {
                        ApplyAccent(window, hwndSource, GetAccentState(d), GetAccentGradientColor(d), GetAccentBorder(d));
                    });
                }
            }
            else if (d is Popup popup)
            {
                popup.Opened -= EventHandlerApplyAccent;
                popup.Opened += EventHandlerApplyAccent;
            }
            else if (d is FrameworkElement element)
            {
                element.Loaded -= EventHandlerApplyAccent;
                element.Loaded += EventHandlerApplyAccent;
            }
        }

        #endregion


        #region Utilities

        private static int CreateColorInteger(Color color)
        {
            return
                color.R << 0 |
                color.G << 8 |
                color.B << 16 |
                color.A << 24;
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

            return null;
        }

        private static void DoAfterHandleOk(DependencyObject dependencyObject, Action<DependencyObject, HwndSource> action)
        {
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

        private static void DoAfterWindowSourceInitialized(Window window, Action action)
        {
            var eventHandler = default(EventHandler);

            eventHandler = (s, e) =>
            {
                action?.Invoke();
                window.SourceInitialized -= eventHandler;
            };

            window.SourceInitialized += eventHandler;
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

        #endregion


        #region Final Logic

        private static unsafe void ApplyBackdrop(Window? window, HwndSource hwndSource, WindowBackdrop backdrop)
        {
            // this api is only available on windows 11 22621
            if (s_versionCurrentWindows < s_versionWindows11_22621)
                return;

            var handle = hwndSource.Handle;

            // prepare composition background
            if (backdrop != WindowBackdrop.None &&
                backdrop != WindowBackdrop.Auto)
            {
                hwndSource.CompositionTarget.BackgroundColor = Colors.Transparent;
            }
            else
            {
                hwndSource.CompositionTarget.BackgroundColor = Colors.Black;
            }

            // prepare glass frame
            if (window is null ||
                WindowChrome.GetWindowChrome(window) is null)
            {
                // TODO: 实现脱离 WindowChrome 的 Blur backdrop 效果
                var margins = new Margins()
                {
                    LeftWidth = -1,
                    RightWidth = -1,
                    TopHeight = -1,
                    BottomHeight = -1
                };

                DwmExtendFrameIntoClientArea(handle, ref margins);
            }

            // set backdrop
            DwmSetWindowAttribute(handle, DwmWindowAttribute.SYSTEMBACKDROP_TYPE, (nint)(void*)&backdrop, (uint)sizeof(WindowBackdrop));

            // draw
            SetWindowPos(
                handle, IntPtr.Zero, 0, 0, 0, 0, 
                SetWindowPosFlags.DRAWFRAME | SetWindowPosFlags.NOACTIVATE | SetWindowPosFlags.NOMOVE | SetWindowPosFlags.NOOWNERZORDER | SetWindowPosFlags.NOSIZE | SetWindowPosFlags.NOZORDER);
        }

        private static unsafe void ApplyCorner(Window? window, HwndSource hwndSource, WindowCorner corner)
        {
            // this api is only available on windows 11 22000
            if (s_versionCurrentWindows < s_versionWindows11_22000)
                return;

            var handle = hwndSource.Handle;

            DwmSetWindowAttribute(handle, DwmWindowAttribute.WINDOW_CORNER_PREFERENCE, (nint)(void*)&corner, (uint)sizeof(WindowCorner));
        }

        private static unsafe void ApplyCaptionColor(Window? window, HwndSource hwndSource, WindowOptionColor color)
        {
            // this api is only available on windows 11 22000
            if (s_versionCurrentWindows < s_versionWindows11_22000)
                return;

            var handle = hwndSource.Handle;

            DwmSetWindowAttribute(handle, DwmWindowAttribute.CAPTION_COLOR, (nint)(void*)&color, (uint)sizeof(WindowOptionColor));
        }

        private static unsafe void ApplyTextColor(Window? window, HwndSource hwndSource, WindowOptionColor color)
        {
            // this api is only available on windows 11 22000
            if (s_versionCurrentWindows < s_versionWindows11_22000)
                return;

            var handle = hwndSource.Handle;

            DwmSetWindowAttribute(handle, DwmWindowAttribute.TEXT_COLOR, (nint)(void*)&color, (uint)sizeof(WindowOptionColor));
        }

        private static unsafe void ApplyBorderColor(Window? window, HwndSource hwndSource, WindowOptionColor color)
        {
            // this api is only available on windows 11 22000
            if (s_versionCurrentWindows < s_versionWindows11_22000)
                return;

            var handle = hwndSource.Handle;

            DwmSetWindowAttribute(handle, DwmWindowAttribute.BORDER_COLOR, (nint)(void*)&color, (uint)sizeof(WindowOptionColor));
        }

        private static unsafe void ApplyDarkMode(Window? window, HwndSource hwndSource, bool isDarkMode)
        {
            // this api is only available on windows 11
            if (s_versionCurrentWindows < s_versionWindows11_22000)
                return;

            var handle = hwndSource.Handle;

            DwmSetWindowAttribute(handle, DwmWindowAttribute.USE_IMMERSIVE_DARK_MODE, (nint)(void*)&isDarkMode, (uint)sizeof(WindowBackdrop));
        }

        private static unsafe void ApplyAccent(Window? window, HwndSource hwndSource, WindowAccentState accentState, Color gradientColor, bool addBorder)
        {
            var handle = hwndSource.Handle;

            var accentPolicy = new AccentPolicy()
            {
                AccentState = (AccentState)accentState,
                AccentFlags = addBorder ? AccentFlags.AllBorder : 0,
                GradientColor = CreateColorInteger(gradientColor)
            };

            var windowCompositionAttributeData = new WindowCompositionAttributeData()
            {
                Attribute = WindowCompositionAttribute.WcaAccentPolicy,
                DataPointer = (nint)(void*)&accentPolicy,
                DataSize = (uint)sizeof(AccentPolicy),
            };

            // prepare composition background
            if (accentState != WindowAccentState.None)
            {
                hwndSource.CompositionTarget.BackgroundColor = Colors.Transparent;
            }
            else
            {
                hwndSource.CompositionTarget.BackgroundColor = Colors.Black;
            }

            // prepare glass frame thickness
            if (window is null ||
                WindowChrome.GetWindowChrome(window) is null)
            {
                var margins = new Margins()
                {
                    LeftWidth = 0,
                    RightWidth = 0,
                    TopHeight = 0,
                    BottomHeight = 0
                };

                // clear margins, composition accent only visible when margin is 0
                DwmExtendFrameIntoClientArea(handle, ref margins);
            }

            // set composition
            SetWindowCompositionAttribute(handle, ref windowCompositionAttributeData);

            // draw
            SetWindowPos(
                handle, IntPtr.Zero, 0, 0, 0, 0, 
                SetWindowPosFlags.DRAWFRAME | SetWindowPosFlags.NOACTIVATE | SetWindowPosFlags.NOMOVE | SetWindowPosFlags.NOOWNERZORDER | SetWindowPosFlags.NOSIZE | SetWindowPosFlags.NOZORDER);
        }

        private static void EventHandlerApplyBackdrop(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyBackdrop(sender as Window, hwndSource, GetBackdrop(d));
        }

        private static void EventHandlerApplyCorner(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyCorner(sender as Window, hwndSource, GetCorner(d));
        }

        private static void EventHandlerApplyCaptionColor(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyCaptionColor(sender as Window, hwndSource, GetCaptionColor(d));
        }

        private static void EventHandlerApplyTextColor(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyTextColor(sender as Window, hwndSource, GetTextColor(d));
        }

        private static void EventHandlerApplyBorderColor(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyBorderColor(sender as Window, hwndSource, GetBorderColor(d));
        }

        private static void EventHandlerApplyDarkMode(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyDarkMode(sender as Window, hwndSource, GetIsDarkMode(d));
        }

        private static void EventHandlerApplyAccent(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyAccent(sender as Window, hwndSource, GetAccentState(d), GetAccentGradientColor(d), GetAccentBorder(d));
        }

        #endregion
    }
}
