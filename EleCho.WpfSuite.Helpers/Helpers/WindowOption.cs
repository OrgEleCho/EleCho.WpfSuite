using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;
using System.Xml.Linq;
using EleCho.WpfSuite.Internal;
using EleCho.WpfSuite.Properties;
using static EleCho.WpfSuite.Helpers.WindowOption.NativeDefinition;

namespace EleCho.WpfSuite.Helpers
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

        static Dictionary<nint, Visual>? s_captions;
        static Dictionary<nint, Visual>? s_maximumButtons;
        static Dictionary<nint, Visual>? s_minimumButtons;
        static Dictionary<nint, Visual>? s_closeButtons;
        static Dictionary<Visual, nint>? s_visualWindows;

        static DependencyPropertyKey s_uiElementIsMouseOverPropertyKey =
            (DependencyPropertyKey)typeof(UIElement).GetField("IsMouseOverPropertyKey", BindingFlags.NonPublic | BindingFlags.Static)!.GetValue(null)!;

        static DependencyPropertyKey s_buttonIsPressedPropertyKey =
            (DependencyPropertyKey)typeof(ButtonBase).GetField("IsPressedPropertyKey", BindingFlags.NonPublic | BindingFlags.Static)!.GetValue(null)!;

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
        /// Get value of IsCaptionVisible property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsCaptionVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCaptionVisibleProperty);
        }

        /// <summary>
        /// Set value of IsCaptionVisible property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsCaptionVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCaptionVisibleProperty, value);
        }


        /// <summary>
        /// Get value of IsCaptionMenuVisible property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsCaptionMenuVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCaptionMenuVisibleProperty);
        }

        /// <summary>
        /// Set value of IsCaptionMenuVisible property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsCaptionMenuVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCaptionMenuVisibleProperty, value);
        }


        /// <summary>
        /// Get value of IsMaximumButton property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsMaximumButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMaximumButtonProperty);
        }

        /// <summary>
        /// Set value of IsMaximumButton property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsMaximumButton(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMaximumButtonProperty, value);
        }


        /// <summary>
        /// Get value of IsMinimumButton property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsMinimumButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMinimumButtonProperty);
        }

        /// <summary>
        /// Set value of IsMinimumButton property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsMinimumButton(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMinimumButtonProperty, value);
        }


        /// <summary>
        /// Get value of IsCloseButton property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsCloseButton(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCloseButtonProperty);
        }

        /// <summary>
        /// Set value of IsCloseButton property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsCloseButton(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCloseButtonProperty, value);
        }


        /// <summary>
        /// Get value of IsCaption property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsCaption(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCaptionProperty);
        }

        /// <summary>
        /// Set value of IsCaption property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsCaption(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCaptionProperty, value);
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

        /// <summary>
        /// The DependencyProperty of IsCaptionVisible property
        /// </summary>
        public static readonly DependencyProperty IsCaptionVisibleProperty =
            DependencyProperty.RegisterAttached("IsCaptionVisible", typeof(bool), typeof(WindowOption), new FrameworkPropertyMetadata(true, OnIsCaptionVisibleChanged));

        /// <summary>
        /// The DependencyProperty of IsCaptionMenuVisible property
        /// </summary>
        public static readonly DependencyProperty IsCaptionMenuVisibleProperty =
            DependencyProperty.RegisterAttached("IsCaptionMenuVisible", typeof(bool), typeof(WindowOption), new FrameworkPropertyMetadata(true, OnIsCaptionMenuVisibleChanged));

        /// <summary>
        /// The DependencyProperty of IsMaximumButton property
        /// </summary>
        public static readonly DependencyProperty IsMaximumButtonProperty =
            DependencyProperty.RegisterAttached("IsMaximumButton", typeof(bool), typeof(WindowOption), new FrameworkPropertyMetadata(false, OnIsMaximumButtonChanged));

        /// <summary>
        /// The DependencyProperty of IsMinimumButton property
        /// </summary>
        public static readonly DependencyProperty IsMinimumButtonProperty =
            DependencyProperty.RegisterAttached("IsMinimumButton", typeof(bool), typeof(WindowOption), new FrameworkPropertyMetadata(false, OnIsMinimumButtonChanged));

        /// <summary>
        /// The DependencyProperty of IsCloseButton property
        /// </summary>
        public static readonly DependencyProperty IsCloseButtonProperty =
            DependencyProperty.RegisterAttached("IsCloseButton", typeof(bool), typeof(WindowOption), new FrameworkPropertyMetadata(false, OnIsCloseButtonChanged));

        /// <summary>
        /// The DependencyProperty of IsCaption property
        /// </summary>
        public static readonly DependencyProperty IsCaptionProperty =
            DependencyProperty.RegisterAttached("IsCaption", typeof(bool), typeof(WindowOption), new FrameworkPropertyMetadata(false, OnIsCaptionChanged));


        private static unsafe IntPtr WindowHitTestInteropHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (handled)
            {
                return IntPtr.Zero;
            }

            switch ((nint)msg)
            {
                case NativeDefinition.WM_NCHITTEST:
                {
                    var xy = *(XYInLParam*)&lParam;
                    var x = (int)xy.X;
                    var y = (int)xy.Y;
                    var result = default(IntPtr);

                    if (s_captions is not null &&
                        s_captions.TryGetValue(hwnd, out var captionVisual))
                    {
                        var relativePoint = captionVisual.PointFromScreen(new Point(x, y));
                        var hitResult = VisualTreeHelper.HitTest(captionVisual, relativePoint);

                        if (hitResult is not null)
                        {
                            captionVisual.SetValue(s_uiElementIsMouseOverPropertyKey, true);

                            handled = true;
                            result = NativeDefinition.HTCAPTION;
                        }
                        else
                        {
                            captionVisual.SetValue(s_uiElementIsMouseOverPropertyKey, false);

                            if (captionVisual is ButtonBase button)
                            {
                                button.SetValue(s_buttonIsPressedPropertyKey, false);
                            }
                        }
                    }

                    if (s_maximumButtons is not null &&
                        s_maximumButtons.TryGetValue(hwnd, out var maximumButtonVisual))
                    {
                        var relativePoint = maximumButtonVisual.PointFromScreen(new Point(x, y));
                        var hitResult = VisualTreeHelper.HitTest(maximumButtonVisual, relativePoint);

                        if (hitResult is not null)
                        {
                            maximumButtonVisual.SetValue(s_uiElementIsMouseOverPropertyKey, true);

                            handled = true;
                            result = NativeDefinition.HTMAXBUTTON;
                        }
                        else
                        {
                            maximumButtonVisual.SetValue(s_uiElementIsMouseOverPropertyKey, false);

                            if (maximumButtonVisual is ButtonBase button)
                            {
                                button.SetValue(s_buttonIsPressedPropertyKey, false);
                            }
                        }
                    }

                    if (s_minimumButtons is not null &&
                        s_minimumButtons.TryGetValue(hwnd, out var minimumButtonVisual))
                    {
                        var relativePoint = minimumButtonVisual.PointFromScreen(new Point(x, y));
                        var hitResult = VisualTreeHelper.HitTest(minimumButtonVisual, relativePoint);

                        if (hitResult is not null)
                        {
                            minimumButtonVisual.SetValue(s_uiElementIsMouseOverPropertyKey, true);

                            handled = true;
                            result = NativeDefinition.HTMINBUTTON;
                        }
                        else
                        {
                            minimumButtonVisual.SetValue(s_uiElementIsMouseOverPropertyKey, false);

                            if (minimumButtonVisual is ButtonBase button)
                            {
                                button.SetValue(s_buttonIsPressedPropertyKey, false);
                            }
                        }
                    }

                    if (s_closeButtons is not null &&
                        s_closeButtons.TryGetValue(hwnd, out var closeButtonVisual))
                    {
                        var relativePoint = closeButtonVisual.PointFromScreen(new Point(x, y));
                        var hitResult = VisualTreeHelper.HitTest(closeButtonVisual, relativePoint);

                        if (hitResult is not null)
                        {
                            closeButtonVisual.SetValue(s_uiElementIsMouseOverPropertyKey, true);

                            handled = true;
                            result = NativeDefinition.HTCLOSE;
                        }
                        else
                        {
                            closeButtonVisual.SetValue(s_uiElementIsMouseOverPropertyKey, false);

                            if (closeButtonVisual is ButtonBase button)
                            {
                                button.SetValue(s_buttonIsPressedPropertyKey, false);
                            }
                        }
                    }

                    return result;
                }

                case NativeDefinition.WM_NCLBUTTONDOWN:
                {
                    var xy = *(XYInLParam*)&lParam;
                    var x = (int)xy.X;
                    var y = (int)xy.Y;

                    if (s_captions is not null &&
                        s_captions.TryGetValue(hwnd, out var captionVisual))
                    {
                        var relativePoint = captionVisual.PointFromScreen(new Point(x, y));
                        var hitResult = VisualTreeHelper.HitTest(captionVisual, relativePoint);

                        if (hitResult is not null)
                        {
                            if (captionVisual is ButtonBase button)
                            {
                                button.SetValue(s_buttonIsPressedPropertyKey, true);
                            }
                        }
                    }

                    if (s_maximumButtons is not null &&
                        s_maximumButtons.TryGetValue(hwnd, out var maximumButtonVisual))
                    {
                        var relativePoint = maximumButtonVisual.PointFromScreen(new Point(x, y));
                        var hitResult = VisualTreeHelper.HitTest(maximumButtonVisual, relativePoint);

                        if (hitResult is not null)
                        {
                            if (maximumButtonVisual is ButtonBase button)
                            {
                                button.SetValue(s_buttonIsPressedPropertyKey, true);
                            }

                            handled = true;
                        }
                    }

                    if (s_minimumButtons is not null &&
                        s_minimumButtons.TryGetValue(hwnd, out var minimumButtonVisual))
                    {
                        var relativePoint = minimumButtonVisual.PointFromScreen(new Point(x, y));
                        var hitResult = VisualTreeHelper.HitTest(minimumButtonVisual, relativePoint);

                        if (hitResult is not null)
                        {
                            if (minimumButtonVisual is ButtonBase button)
                            {
                                button.SetValue(s_buttonIsPressedPropertyKey, true);
                            }

                            handled = true;
                        }
                    }

                    if (s_closeButtons is not null &&
                        s_closeButtons.TryGetValue(hwnd, out var closeButtonVisual))
                    {
                        var relativePoint = closeButtonVisual.PointFromScreen(new Point(x, y));
                        var hitResult = VisualTreeHelper.HitTest(closeButtonVisual, relativePoint);

                        if (hitResult is not null)
                        {
                            if (closeButtonVisual is ButtonBase button)
                            {
                                button.SetValue(s_buttonIsPressedPropertyKey, true);
                            }

                            handled = true;
                        }
                    }

                    break;
                }

                case NativeDefinition.WM_NCLBUTTONUP:
                {
                    var xy = *(XYInLParam*)&lParam;
                    var x = (int)xy.X;
                    var y = (int)xy.Y;

                    if (s_captions is not null &&
                        s_captions.TryGetValue(hwnd, out var captionVisual))
                    {
                        if (captionVisual is ButtonBase button)
                        {
                            bool shouldClick = false;
                            if ((bool)button.GetValue(s_buttonIsPressedPropertyKey.DependencyProperty))
                            {
                                shouldClick = true;
                            }

                            button.SetValue(s_buttonIsPressedPropertyKey, false);

                            if (shouldClick)
                            {
                                button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, button));
                                button.Command?.Execute(button.CommandParameter);
                            }
                        }
                    }

                    if (s_maximumButtons is not null &&
                        s_maximumButtons.TryGetValue(hwnd, out var maximumButtonVisual))
                    {
                        if (maximumButtonVisual is ButtonBase button)
                        {
                            bool shouldClick = false;
                            if ((bool)button.GetValue(s_buttonIsPressedPropertyKey.DependencyProperty))
                            {
                                shouldClick = true;
                            }

                            button.SetValue(s_buttonIsPressedPropertyKey, false);

                            if (shouldClick)
                            {
                                button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, button));
                                button.Command?.Execute(button.CommandParameter);
                            }

                            handled = true;
                        }
                    }

                    if (s_minimumButtons is not null &&
                        s_minimumButtons.TryGetValue(hwnd, out var minimumButtonVisual))
                    {
                        if (minimumButtonVisual is ButtonBase button)
                        {
                            bool shouldClick = false;
                            if ((bool)button.GetValue(s_buttonIsPressedPropertyKey.DependencyProperty))
                            {
                                shouldClick = true;
                            }

                            button.SetValue(s_buttonIsPressedPropertyKey, false);

                            if (shouldClick)
                            {
                                button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, button));
                                button.Command?.Execute(button.CommandParameter);
                            }

                            handled = true;
                        }
                    }

                    if (s_closeButtons is not null &&
                        s_closeButtons.TryGetValue(hwnd, out var closeButtonVisual))
                    {
                        if (closeButtonVisual is ButtonBase button)
                        {
                            bool shouldClick = false;
                            if ((bool)button.GetValue(s_buttonIsPressedPropertyKey.DependencyProperty))
                            {
                                shouldClick = true;
                            }

                            button.SetValue(s_buttonIsPressedPropertyKey, false);

                            if (shouldClick)
                            {
                                button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, button));
                                button.Command?.Execute(button.CommandParameter);
                            }

                            handled = true;
                        }
                    }

                    break;
                }

                case NativeDefinition.WM_NCMOUSELEAVE:
                {
                    var xy = *(XYInLParam*)&lParam;
                    var x = (int)xy.X;
                    var y = (int)xy.Y;

                    if (s_captions is not null &&
                        s_captions.TryGetValue(hwnd, out var captionVisual))
                    {
                        captionVisual.SetValue(s_uiElementIsMouseOverPropertyKey, false);

                        if (captionVisual is ButtonBase button)
                        {
                            button.SetValue(s_buttonIsPressedPropertyKey, false);
                        }
                    }

                    if (s_maximumButtons is not null &&
                        s_maximumButtons.TryGetValue(hwnd, out var maximumButtonVisual))
                    {
                        maximumButtonVisual.SetValue(s_uiElementIsMouseOverPropertyKey, false);

                        if (maximumButtonVisual is ButtonBase button)
                        {
                            button.SetValue(s_buttonIsPressedPropertyKey, false);
                        }
                    }

                    if (s_minimumButtons is not null &&
                        s_minimumButtons.TryGetValue(hwnd, out var minimumButtonVisual))
                    {
                        minimumButtonVisual.SetValue(s_uiElementIsMouseOverPropertyKey, false);

                        if (minimumButtonVisual is ButtonBase button)
                        {
                            button.SetValue(s_buttonIsPressedPropertyKey, false);
                        }
                    }

                    if (s_closeButtons is not null &&
                        s_closeButtons.TryGetValue(hwnd, out var closeButtonVisual))
                    {
                        closeButtonVisual.SetValue(s_uiElementIsMouseOverPropertyKey, false);

                        if (closeButtonVisual is ButtonBase button)
                        {
                            button.SetValue(s_buttonIsPressedPropertyKey, false);
                        }
                    }

                    break;
                }
            }

            return IntPtr.Zero;
        }

        #region Event Handlers

        private static void OnMinimumButtonLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement frameworkElement)
            {
                return;
            }

            if (GetWindowHwndSource(frameworkElement) is not HwndSource hwndSource)
            {
                throw new InvalidOperationException(StringResources.CanNotGetHwndSourceOfVisual);
            }

            ApplyWindowMinimumButton(hwndSource, frameworkElement, true);
        }

        private static void OnMinimumButtonUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement frameworkElement)
            {
                return;
            }

            ApplyWindowMinimumButton(null, frameworkElement, false);
        }

        private static void OnMaximumButtonLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement frameworkElement)
            {
                return;
            }

            if (GetWindowHwndSource(frameworkElement) is not HwndSource hwndSource)
            {
                throw new InvalidOperationException(StringResources.CanNotGetHwndSourceOfVisual);
            }

            ApplyWindowMaximumButton(hwndSource, frameworkElement, true);
        }

        private static void OnMaximumButtonUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement frameworkElement)
            {
                return;
            }

            ApplyWindowMaximumButton(null, frameworkElement, false);
        }

        private static void OnCloseButtonLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement frameworkElement)
            {
                return;
            }

            if (GetWindowHwndSource(frameworkElement) is not HwndSource hwndSource)
            {
                throw new InvalidOperationException(StringResources.CanNotGetHwndSourceOfVisual);
            }

            ApplyWindowCloseButton(hwndSource, frameworkElement, true);
        }

        private static void OnCloseButtonUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement frameworkElement)
            {
                return;
            }

            ApplyWindowCloseButton(null, frameworkElement, false);
        }

        private static void OnCaptionLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement frameworkElement)
            {
                return;
            }

            if (GetWindowHwndSource(frameworkElement) is not HwndSource hwndSource)
            {
                throw new InvalidOperationException(StringResources.CanNotGetHwndSourceOfVisual);
            }

            ApplyWindowCaption(hwndSource, frameworkElement, true);
        }

        private static void OnCaptionUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement frameworkElement)
            {
                return;
            }

            ApplyWindowCaption(null, frameworkElement, false);
        }

        #endregion


        #region DependencyProperty Callbacks

        private static void OnBackdropChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var accentState = GetAccentState(d);
            var backdrop = GetBackdrop(d);

            // check
            if (accentState is not WindowAccentState.None &&
                backdrop is not WindowBackdrop.Auto &&
                backdrop is not WindowBackdrop.None)
            {
                throw new InvalidOperationException(StringResources.BackdropAndAccentCannotBothBeSet);
            }

            if (GetWindowHwndSource(d) is HwndSource hwndSource)
            {
                ApplyBackdrop(d as Window, hwndSource, backdrop);
            }

            if (backdrop != WindowBackdrop.Auto)
            {
                if (d is Window window)
                {
                    window.SourceInitialized += EventHandlerApplyBackdrop;
                }
                else if (d is Popup popup)
                {
                    popup.Opened += EventHandlerApplyBackdrop;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened += EventHandlerApplyBackdrop;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened += EventHandlerApplyBackdrop;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened += EventHandlerApplyBackdrop;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded += EventHandlerApplyBackdrop;
                }
            }
            else
            {
                if (d is Window window)
                {
                    window.SourceInitialized -= EventHandlerApplyBackdrop;
                }
                else if (d is Popup popup)
                {
                    popup.Opened -= EventHandlerApplyBackdrop;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened -= EventHandlerApplyBackdrop;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened -= EventHandlerApplyBackdrop;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened -= EventHandlerApplyBackdrop;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded -= EventHandlerApplyBackdrop;
                }
            }
        }

        private static void OnCornerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var corner = GetCorner(d);

            if (GetWindowHwndSource(d) is HwndSource hwndSource)
            {
                ApplyCorner(hwndSource, corner);
            }

            if (corner != WindowCorner.Default)
            {
                if (d is Window window)
                {
                    window.SourceInitialized += EventHandlerApplyCorner;
                }
                else if (d is Popup popup)
                {
                    popup.Opened += EventHandlerApplyCorner;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened += EventHandlerApplyCorner;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened += EventHandlerApplyCorner;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened += EventHandlerApplyCorner;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded += EventHandlerApplyCorner;
                }
            }
            else
            {
                if (d is Window window)
                {
                    window.SourceInitialized -= EventHandlerApplyCorner;
                }
                else if (d is Popup popup)
                {
                    popup.Opened -= EventHandlerApplyCorner;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened -= EventHandlerApplyCorner;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened -= EventHandlerApplyCorner;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened -= EventHandlerApplyCorner;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded -= EventHandlerApplyCorner;
                }
            }
        }

        private static void OnCaptionColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var captionColor = GetCaptionColor(d);

            if (GetWindowHwndSource(d) is HwndSource hwndSource)
            {
                ApplyCaptionColor(hwndSource, captionColor);
            }

            if (captionColor != WindowOptionColor.Default)
            {
                if (d is Window window)
                {
                    window.SourceInitialized += EventHandlerApplyCaptionColor;
                }
                else if (d is Popup popup)
                {
                    popup.Opened += EventHandlerApplyCaptionColor;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened += EventHandlerApplyCaptionColor;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened += EventHandlerApplyCaptionColor;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened += EventHandlerApplyCaptionColor;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded += EventHandlerApplyCaptionColor;
                }
            }
            else
            {
                if (d is Window window)
                {
                    window.SourceInitialized -= EventHandlerApplyCaptionColor;
                }
                else if (d is Popup popup)
                {
                    popup.Opened -= EventHandlerApplyCaptionColor;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened -= EventHandlerApplyCaptionColor;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened -= EventHandlerApplyCaptionColor;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened -= EventHandlerApplyCaptionColor;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded -= EventHandlerApplyCaptionColor;
                }
            }
        }

        private static void OnTextColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textColor = GetTextColor(d);

            if (GetWindowHwndSource(d) is HwndSource hwndSource)
            {
                ApplyTextColor(hwndSource, textColor);
            }

            if (textColor != WindowOptionColor.Default)
            {
                if (d is Window window)
                {
                    window.SourceInitialized += EventHandlerApplyTextColor;
                }
                else if (d is Popup popup)
                {
                    popup.Opened += EventHandlerApplyTextColor;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened += EventHandlerApplyTextColor;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened += EventHandlerApplyTextColor;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened += EventHandlerApplyTextColor;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded += EventHandlerApplyTextColor;
                }
            }
            else
            {
                if (d is Window window)
                {
                    window.SourceInitialized -= EventHandlerApplyTextColor;
                }
                else if (d is Popup popup)
                {
                    popup.Opened -= EventHandlerApplyTextColor;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened -= EventHandlerApplyTextColor;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened -= EventHandlerApplyTextColor;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened -= EventHandlerApplyTextColor;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded -= EventHandlerApplyTextColor;
                }
            }
        }

        private static void OnBorderColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var borderColor = GetBorderColor(d);

            if (GetWindowHwndSource(d) is HwndSource hwndSource)
            {
                ApplyBorderColor(hwndSource, borderColor);
            }

            if (borderColor != WindowOptionColor.Default)
            {
                if (d is Window window)
                {
                    window.SourceInitialized += EventHandlerApplyBorderColor;
                }
                else if (d is Popup popup)
                {
                    popup.Opened += EventHandlerApplyBorderColor;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened += EventHandlerApplyBorderColor;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened += EventHandlerApplyBorderColor;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened += EventHandlerApplyBorderColor;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded += EventHandlerApplyBorderColor;
                }
            }
            else
            {
                if (d is Window window)
                {
                    window.SourceInitialized -= EventHandlerApplyBorderColor;
                }
                else if (d is Popup popup)
                {
                    popup.Opened -= EventHandlerApplyBorderColor;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened -= EventHandlerApplyBorderColor;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened -= EventHandlerApplyBorderColor;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened -= EventHandlerApplyBorderColor;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded -= EventHandlerApplyBorderColor;
                }
            }
        }

        private static void OnIsDarkModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var isDarkMode = GetIsDarkMode(d);

            if (GetWindowHwndSource(d) is HwndSource hwndSource)
            {
                ApplyDarkMode(hwndSource, isDarkMode);
            }

            if (isDarkMode)
            {
                if (d is Window window)
                {
                    window.SourceInitialized += EventHandlerApplyDarkMode;
                }
                else if (d is Popup popup)
                {
                    popup.Opened += EventHandlerApplyDarkMode;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened += EventHandlerApplyDarkMode;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened += EventHandlerApplyDarkMode;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened += EventHandlerApplyDarkMode;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded += EventHandlerApplyDarkMode;
                }
            }
            else
            {
                if (d is Window window)
                {
                    window.SourceInitialized -= EventHandlerApplyDarkMode;
                }
                else if (d is Popup popup)
                {
                    popup.Opened -= EventHandlerApplyDarkMode;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened -= EventHandlerApplyDarkMode;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened -= EventHandlerApplyDarkMode;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened -= EventHandlerApplyDarkMode;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded -= EventHandlerApplyDarkMode;
                }
            }
        }

        private static void OnAccentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // check
            if (GetAccentState(d) is not WindowAccentState.None &&
                GetBackdrop(d) is not WindowBackdrop.Auto &&
                GetBackdrop(d) is not WindowBackdrop.None)
            {
                throw new InvalidOperationException(StringResources.BackdropAndAccentCannotBothBeSet);
            }

            var accentState = GetAccentState(d);
            var gradientColor = GetAccentGradientColor(d);
            var accentBorder = GetAccentBorder(d);

            if (GetWindowHwndSource(d) is HwndSource hwndSource)
            {
                ApplyAccent(d as Window, hwndSource, accentState, gradientColor, accentBorder);
            }

            if (accentState != WindowAccentState.None)
            {
                if (d is Window window)
                {
                    window.SourceInitialized += EventHandlerApplyAccent;
                }
                else if (d is Popup popup)
                {
                    popup.Opened += EventHandlerApplyAccent;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened += EventHandlerApplyAccent;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened += EventHandlerApplyAccent;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened += EventHandlerApplyAccent;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded += EventHandlerApplyAccent;
                }
            }
            else
            {
                if (d is Window window)
                {
                    window.SourceInitialized -= EventHandlerApplyAccent;
                }
                else if (d is Popup popup)
                {
                    popup.Opened -= EventHandlerApplyAccent;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened -= EventHandlerApplyAccent;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened -= EventHandlerApplyAccent;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened -= EventHandlerApplyAccent;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded -= EventHandlerApplyAccent;
                }
            }
        }

        private static void OnIsCaptionVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var isCaptionVisible = GetIsCaptionVisible(d);

            if (GetWindowHwndSource(d) is HwndSource hwndSource)
            {
                ApplyIsCaptionVisible(hwndSource, isCaptionVisible);
            }

            if (!isCaptionVisible)
            {
                if (d is Window window)
                {
                    window.SourceInitialized += EventHandlerApplyIsCaptionVisible;
                }
                else if (d is Popup popup)
                {
                    popup.Opened += EventHandlerApplyIsCaptionVisible;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened += EventHandlerApplyIsCaptionVisible;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened += EventHandlerApplyIsCaptionVisible;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened += EventHandlerApplyIsCaptionVisible;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded += EventHandlerApplyIsCaptionVisible;
                }
            }
            else
            {
                if (d is Window window)
                {
                    window.SourceInitialized -= EventHandlerApplyIsCaptionVisible;
                }
                else if (d is Popup popup)
                {
                    popup.Opened -= EventHandlerApplyIsCaptionVisible;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened -= EventHandlerApplyIsCaptionVisible;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened -= EventHandlerApplyIsCaptionVisible;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened -= EventHandlerApplyIsCaptionVisible;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded -= EventHandlerApplyIsCaptionVisible;
                }
            }
        }

        private static void OnIsCaptionMenuVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var isCaptionMenuVisible = GetIsCaptionMenuVisible(d);

            if (GetWindowHwndSource(d) is HwndSource hwndSource)
            {
                ApplyIsCaptionMenuVisible(hwndSource, isCaptionMenuVisible);
            }

            if (!isCaptionMenuVisible)
            {
                if (d is Window window)
                {
                    window.SourceInitialized += EventHandlerApplyIsCaptionMenuVisible;
                }
                else if (d is Popup popup)
                {
                    popup.Opened += EventHandlerApplyIsCaptionMenuVisible;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened += EventHandlerApplyIsCaptionMenuVisible;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened += EventHandlerApplyIsCaptionMenuVisible;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened += EventHandlerApplyIsCaptionMenuVisible;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded += EventHandlerApplyIsCaptionMenuVisible;
                }
            }
            else
            {
                if (d is Window window)
                {
                    window.SourceInitialized -= EventHandlerApplyIsCaptionMenuVisible;
                }
                else if (d is Popup popup)
                {
                    popup.Opened -= EventHandlerApplyIsCaptionMenuVisible;
                }
                else if (d is ToolTip toolTip)
                {
                    toolTip.Opened -= EventHandlerApplyIsCaptionMenuVisible;
                }
                else if (d is ContextMenu contextMenu)
                {
                    contextMenu.Opened -= EventHandlerApplyIsCaptionMenuVisible;
                }
                else if (d is MenuItem menuItem)
                {
                    menuItem.SubmenuOpened -= EventHandlerApplyIsCaptionMenuVisible;
                }
                else if (d is FrameworkElement element)
                {
                    element.Loaded -= EventHandlerApplyIsCaptionMenuVisible;
                }
            }
        }

        private static void OnIsMinimumButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement frameworkElement)
            {
                throw new InvalidOperationException("Target DependencyObject is not FrameworkElement");
            }

            if (DesignerProperties.GetIsInDesignMode(d))
            {
                return;
            }

            var newValue = (bool)e.NewValue;

            if (newValue)
            {
                if (GetIsMaximumButton(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowMaximumButton);
                }

                if (GetIsCloseButton(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowCloseButton);
                }

                if (GetIsCaption(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowCaption);
                }

                frameworkElement.Loaded += OnMinimumButtonLoaded;
                frameworkElement.Unloaded += OnMinimumButtonUnloaded;

                if (frameworkElement.IsLoaded &&
                    GetWindowHwndSource(frameworkElement) is HwndSource hwndSource)
                {
                    ApplyWindowMinimumButton(hwndSource, frameworkElement, true);
                }
            }
            else
            {
                frameworkElement.Loaded += OnMinimumButtonLoaded;
                frameworkElement.Unloaded += OnMinimumButtonUnloaded;

                if (frameworkElement.IsLoaded &&
                    GetWindowHwndSource(frameworkElement) is HwndSource hwndSource)
                {
                    ApplyWindowMinimumButton(hwndSource, frameworkElement, false);
                }
            }
        }

        private static void OnIsMaximumButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement frameworkElement)
            {
                throw new InvalidOperationException("Target DependencyObject is not FrameworkElement");
            }

            if (DesignerProperties.GetIsInDesignMode(d))
            {
                return;
            }

            var newValue = (bool)e.NewValue;

            if (newValue)
            {
                if (GetIsMinimumButton(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowMinimumButton);
                }

                if (GetIsCloseButton(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowCloseButton);
                }

                if (GetIsCaption(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowCaption);
                }

                frameworkElement.Loaded += OnMaximumButtonLoaded;
                frameworkElement.Unloaded += OnMaximumButtonUnloaded;

                if (frameworkElement.IsLoaded &&
                    GetWindowHwndSource(frameworkElement) is HwndSource hwndSource)
                {
                    ApplyWindowMaximumButton(hwndSource, frameworkElement, true);
                }
            }
            else
            {
                frameworkElement.Loaded += OnMaximumButtonLoaded;
                frameworkElement.Unloaded += OnMaximumButtonUnloaded;

                if (frameworkElement.IsLoaded &&
                    GetWindowHwndSource(frameworkElement) is HwndSource hwndSource)
                {
                    ApplyWindowMaximumButton(hwndSource, frameworkElement, false);
                }
            }
        }

        private static void OnIsCloseButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement frameworkElement)
            {
                throw new InvalidOperationException("Target DependencyObject is not FrameworkElement");
            }

            if (DesignerProperties.GetIsInDesignMode(d))
            {
                return;
            }

            var newValue = (bool)e.NewValue;

            if (newValue)
            {
                if (GetIsMinimumButton(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowMinimumButton);
                }

                if (GetIsMaximumButton(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowMaximumButton);
                }

                if (GetIsCaption(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowCaption);
                }

                frameworkElement.Loaded += OnCloseButtonLoaded;
                frameworkElement.Unloaded += OnCloseButtonUnloaded;

                if (frameworkElement.IsLoaded &&
                    GetWindowHwndSource(frameworkElement) is HwndSource hwndSource)
                {
                    ApplyWindowCloseButton(hwndSource, frameworkElement, true);
                }
            }
            else
            {
                frameworkElement.Loaded += OnCloseButtonLoaded;
                frameworkElement.Unloaded += OnCloseButtonUnloaded;

                if (frameworkElement.IsLoaded &&
                    GetWindowHwndSource(frameworkElement) is HwndSource hwndSource)
                {
                    ApplyWindowCloseButton(hwndSource, frameworkElement, false);
                }
            }
        }

        private static void OnIsCaptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement frameworkElement)
            {
                throw new InvalidOperationException("Target DependencyObject is not FrameworkElement");
            }

            if (DesignerProperties.GetIsInDesignMode(d))
            {
                return;
            }

            var newValue = (bool)e.NewValue;

            if (newValue)
            {
                if (GetIsMinimumButton(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowMinimumButton);
                }

                if (GetIsMaximumButton(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowMaximumButton);
                }

                if (GetIsCloseButton(d))
                {
                    throw new InvalidOperationException(StringResources.ThisVisualIsAlreadyAWindowCloseButton);
                }

                frameworkElement.Loaded += OnCaptionLoaded;
                frameworkElement.Unloaded += OnCaptionUnloaded;

                if (frameworkElement.IsLoaded &&
                    GetWindowHwndSource(frameworkElement) is HwndSource hwndSource)
                {
                    ApplyWindowCaption(hwndSource, frameworkElement, true);
                }
            }
            else
            {
                frameworkElement.Loaded += OnCaptionLoaded;
                frameworkElement.Unloaded += OnCaptionUnloaded;

                if (frameworkElement.IsLoaded &&
                    GetWindowHwndSource(frameworkElement) is HwndSource hwndSource)
                {
                    ApplyWindowCaption(hwndSource, frameworkElement, false);
                }
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
            else if (VisualTreeUtils.FindChild<Popup>(dependencyObject) is Popup childPopup)
            {
                if (childPopup.Child is null)
                    return null;

                return PresentationSource.FromVisual(childPopup.Child) as HwndSource;
            }
            else if (dependencyObject is Visual visual)
            {
                return PresentationSource.FromVisual(visual) as HwndSource;
            }

            return null;
        }

        private static void UnregisterEventHandlerForWindowHandleOk(Visual visual, EventHandler eventHandler)
        {

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
            else if (dependencyObject is ContextMenu contextMenu)
            {
                var eventHandler = default(RoutedEventHandler);

                eventHandler = (s, e) =>
                {
                    if (GetWindowHwndSource(contextMenu) is HwndSource hwndSource)
                    {
                        action?.Invoke(dependencyObject, hwndSource);
                    }

                    contextMenu.Opened -= eventHandler;
                };

                contextMenu.Opened += eventHandler;
            }
            else if (dependencyObject is MenuItem menuItem)
            {
                var eventHandler = default(RoutedEventHandler);

                eventHandler = (s, e) =>
                {
                    if (GetWindowHwndSource(menuItem) is HwndSource hwndSource)
                    {
                        action?.Invoke(dependencyObject, hwndSource);
                    }

                    menuItem.SubmenuOpened -= eventHandler;
                };

                menuItem.SubmenuOpened += eventHandler;
            }
            else
            {
                throw new NotSupportedException("Invalid dependency object");
            }
        }

        private static bool HasWindowCaptionButton(nint hwnd)
        {
            if (s_minimumButtons is not null && s_minimumButtons.ContainsKey(hwnd))
                return true;
            if (s_maximumButtons is not null && s_maximumButtons.ContainsKey(hwnd))
                return true;
            if (s_closeButtons is not null && s_closeButtons.ContainsKey(hwnd))
                return true;

            return false;
        }

        #endregion


        #region Final Logic

        private static unsafe void ApplyBackdrop(Window? window, HwndSource hwndSource, WindowBackdrop backdrop)
        {
            // this api is only available on windows 11 22621
            if (s_versionCurrentWindows < s_versionWindows11_22621)
                return;

            var debugMode = InternalUtilities.IsApplicationInDebugMode();
            var handle = hwndSource.Handle;

            if (debugMode && window is not null)
            {
                if (window.Background is SolidColorBrush solidColorBackground &&
                    solidColorBackground.Color.A == 255)
                {
                    throw new InvalidOperationException(
                        $"""
                        {StringResources.YouCanOnlySeeTheEffectOfTheBackdropSettingWhenTheBackgroundIsSetToATransparentBrush} ({StringResources.ThisExceptionIsOnlyThrownWhenTheProgramIsRunningInDebugMode})
                        """);
                }

                if (WindowChrome.GetWindowChrome(window) is { } windowChrome &&
                    windowChrome.GlassFrameThickness.Left == 0 &&
                    windowChrome.GlassFrameThickness.Right == 0 &&
                    windowChrome.GlassFrameThickness.Top == 0 &&
                    windowChrome.GlassFrameThickness.Bottom == 0)
                {
                    throw new InvalidOperationException(
                        $"""
                        {StringResources.IfYouSetTheBackdropInTheCaseOfUsingWindowChromeYouWillOnlySeeTheEffectWhenGlassFrameThicknessIsANonZeroValueGenerallyItShouldBeSetToNegativeOne} ({StringResources.ThisExceptionIsOnlyThrownWhenTheProgramIsRunningInDebugMode})
                        """);
                }
            }

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

        private static unsafe void ApplyCorner(HwndSource hwndSource, WindowCorner corner)
        {
            // this api is only available on windows 11 22000
            if (s_versionCurrentWindows < s_versionWindows11_22000)
                return;

            var handle = hwndSource.Handle;

            DwmSetWindowAttribute(handle, DwmWindowAttribute.WINDOW_CORNER_PREFERENCE, (nint)(void*)&corner, (uint)sizeof(WindowCorner));
        }

        private static unsafe void ApplyCaptionColor(HwndSource hwndSource, WindowOptionColor color)
        {
            // this api is only available on windows 11 22000
            if (s_versionCurrentWindows < s_versionWindows11_22000)
                return;

            var handle = hwndSource.Handle;

            DwmSetWindowAttribute(handle, DwmWindowAttribute.CAPTION_COLOR, (nint)(void*)&color, (uint)sizeof(WindowOptionColor));
        }

        private static unsafe void ApplyTextColor(HwndSource hwndSource, WindowOptionColor color)
        {
            // this api is only available on windows 11 22000
            if (s_versionCurrentWindows < s_versionWindows11_22000)
                return;

            var handle = hwndSource.Handle;

            DwmSetWindowAttribute(handle, DwmWindowAttribute.TEXT_COLOR, (nint)(void*)&color, (uint)sizeof(WindowOptionColor));
        }

        private static unsafe void ApplyBorderColor(HwndSource hwndSource, WindowOptionColor color)
        {
            // this api is only available on windows 11 22000
            if (s_versionCurrentWindows < s_versionWindows11_22000)
                return;

            var handle = hwndSource.Handle;

            DwmSetWindowAttribute(handle, DwmWindowAttribute.BORDER_COLOR, (nint)(void*)&color, (uint)sizeof(WindowOptionColor));
        }

        private static unsafe void ApplyDarkMode(HwndSource hwndSource, bool isDarkMode)
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

            var debugMode = InternalUtilities.IsApplicationInDebugMode();

            if (debugMode && window is not null)
            {
                if (WindowChrome.GetWindowChrome(window) is { } windowChrome &&
                    windowChrome.GlassFrameThickness != default)
                {
                    throw new InvalidOperationException(
                        $"""
                        {StringResources.IfYouSetTheAccentWhileUsingWindowChromeYouCanOnlySeeTheEffectWhenTheGlassFrameThicknessIsSetToZero} ({StringResources.ThisExceptionIsOnlyThrownWhenTheProgramIsRunningInDebugMode})
                        """);
                }
            }

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

            // refresh
            SetWindowPos(
                handle, IntPtr.Zero, 0, 0, 0, 0,
                SetWindowPosFlags.DRAWFRAME | SetWindowPosFlags.NOACTIVATE | SetWindowPosFlags.NOMOVE | SetWindowPosFlags.NOOWNERZORDER | SetWindowPosFlags.NOSIZE | SetWindowPosFlags.NOZORDER);
        }

        private static unsafe void ApplyIsCaptionVisible(HwndSource hwndSource, bool isCaptionVisible)
        {
            var handle = hwndSource.Handle;

            var oldPtr = NativeDefinition.GetWindowLong(handle, NativeDefinition.GWL_STYLE);
            var newPtr = isCaptionVisible ?
                oldPtr | NativeDefinition.WS_CAPTION :
                oldPtr & ~NativeDefinition.WS_CAPTION;

            NativeDefinition.SetWindowLong(handle, NativeDefinition.GWL_STYLE, newPtr);

            // refresh
            SetWindowPos(
                handle, IntPtr.Zero, 0, 0, 0, 0,
                SetWindowPosFlags.DRAWFRAME | SetWindowPosFlags.NOACTIVATE | SetWindowPosFlags.NOMOVE | SetWindowPosFlags.NOOWNERZORDER | SetWindowPosFlags.NOSIZE | SetWindowPosFlags.NOZORDER);
        }

        private static unsafe void ApplyIsCaptionMenuVisible(HwndSource hwndSource, bool isCaptionMenuVisible)
        {
            var handle = hwndSource.Handle;

            var oldPtr = NativeDefinition.GetWindowLong(handle, NativeDefinition.GWL_STYLE);
            var newPtr = isCaptionMenuVisible ?
                oldPtr | NativeDefinition.WS_SYSMENU :
                oldPtr & ~NativeDefinition.WS_SYSMENU;

            NativeDefinition.SetWindowLong(handle, NativeDefinition.GWL_STYLE, newPtr);

            // refresh
            SetWindowPos(
                handle, IntPtr.Zero, 0, 0, 0, 0,
                SetWindowPosFlags.DRAWFRAME | SetWindowPosFlags.NOACTIVATE | SetWindowPosFlags.NOMOVE | SetWindowPosFlags.NOOWNERZORDER | SetWindowPosFlags.NOSIZE | SetWindowPosFlags.NOZORDER);
        }

        private static unsafe void ApplyWindowMinimumButton(HwndSource? hwndSource, Visual visual, bool isMinimumButton)
        {
            if (isMinimumButton)
            {
                if (hwndSource is null)
                {
                    throw new ArgumentNullException(nameof(hwndSource));
                }

                var windowHandle = hwndSource.Handle;

                s_minimumButtons ??= new();
                s_visualWindows ??= new();

                if (s_minimumButtons.ContainsKey(windowHandle))
                {
                    throw new InvalidOperationException(StringResources.MinimumButtonIsAlreadySetToAnotherVisual);
                }

                bool hasHookBefore = HasWindowCaptionButton(windowHandle);

                s_minimumButtons[windowHandle] = visual;
                s_visualWindows[visual] = windowHandle;

                if (!hasHookBefore)
                {
                    hwndSource.AddHook(WindowHitTestInteropHook);
                }
            }
            else
            {
                if (s_minimumButtons is null ||
                    s_visualWindows is null)
                {
                    return;
                }

                if (hwndSource is null)
                {
                    if (!s_visualWindows.TryGetValue(visual, out var handle))
                    {
                        throw new InvalidOperationException();
                    }

                    hwndSource = HwndSource.FromHwnd(handle);
                }

                var windowHandle = hwndSource.Handle;

                s_minimumButtons.Remove(windowHandle);
                s_visualWindows.Remove(visual);

                if (s_minimumButtons.Count == 0)
                {
                    s_minimumButtons = null;
                }

                if (s_visualWindows.Count == 0)
                {
                    s_visualWindows = null;
                }

                if (!HasWindowCaptionButton(windowHandle))
                {
                    hwndSource.RemoveHook(WindowHitTestInteropHook);
                }
            }
        }

        private static unsafe void ApplyWindowMaximumButton(HwndSource? hwndSource, Visual visual, bool isMaximumButton)
        {
            if (isMaximumButton)
            {
                if (hwndSource is null)
                {
                    throw new ArgumentNullException(nameof(hwndSource));
                }

                var windowHandle = hwndSource.Handle;

                s_maximumButtons ??= new();
                s_visualWindows ??= new();

                if (s_maximumButtons.ContainsKey(windowHandle))
                {
                    throw new InvalidOperationException(StringResources.MaximumButtonIsAlreadySetToAnotherVisual);
                }

                bool hasHookBefore = HasWindowCaptionButton(windowHandle);

                s_maximumButtons[windowHandle] = visual;
                s_visualWindows[visual] = windowHandle;

                if (!hasHookBefore)
                {
                    hwndSource.AddHook(WindowHitTestInteropHook);
                }
            }
            else
            {
                if (s_maximumButtons is null ||
                    s_visualWindows is null)
                {
                    return;
                }

                if (hwndSource is null)
                {
                    if (!s_visualWindows.TryGetValue(visual, out var handle))
                    {
                        throw new InvalidOperationException();
                    }

                    hwndSource = HwndSource.FromHwnd(handle);
                }

                var windowHandle = hwndSource.Handle;

                s_maximumButtons.Remove(windowHandle);
                s_visualWindows.Remove(visual);

                if (s_maximumButtons.Count == 0)
                {
                    s_maximumButtons = null;
                }

                if (s_visualWindows.Count == 0)
                {
                    s_visualWindows = null;
                }

                if (!HasWindowCaptionButton(windowHandle))
                {
                    hwndSource.RemoveHook(WindowHitTestInteropHook);
                }
            }
        }

        private static unsafe void ApplyWindowCloseButton(HwndSource? hwndSource, Visual visual, bool isCloseButton)
        {
            if (isCloseButton)
            {
                if (hwndSource is null)
                {
                    throw new ArgumentNullException(nameof(hwndSource));
                }

                var windowHandle = hwndSource.Handle;

                s_closeButtons ??= new();
                s_visualWindows ??= new();

                if (s_closeButtons.ContainsKey(windowHandle))
                {
                    throw new InvalidOperationException(StringResources.CloseButtonIsAlreadySetToAnotherVisual);
                }

                bool hasHookBefore = HasWindowCaptionButton(windowHandle);

                s_closeButtons[windowHandle] = visual;
                s_visualWindows[visual] = windowHandle;

                if (!hasHookBefore)
                {
                    hwndSource.AddHook(WindowHitTestInteropHook);
                }
            }
            else
            {
                if (s_closeButtons is null ||
                    s_visualWindows is null)
                {
                    return;
                }

                if (hwndSource is null)
                {
                    if (!s_visualWindows.TryGetValue(visual, out var handle))
                    {
                        throw new InvalidOperationException();
                    }

                    hwndSource = HwndSource.FromHwnd(handle);
                }

                var windowHandle = hwndSource.Handle;

                s_closeButtons.Remove(windowHandle);
                s_visualWindows.Remove(visual);

                if (s_closeButtons.Count == 0)
                {
                    s_closeButtons = null;
                }

                if (s_visualWindows.Count == 0)
                {
                    s_visualWindows = null;
                }

                if (!HasWindowCaptionButton(windowHandle))
                {
                    hwndSource.RemoveHook(WindowHitTestInteropHook);
                }
            }
        }

        private static unsafe void ApplyWindowCaption(HwndSource? hwndSource, Visual visual, bool isCaption)
        {
            if (isCaption)
            {
                if (hwndSource is null)
                {
                    throw new ArgumentNullException(nameof(hwndSource));
                }

                var windowHandle = hwndSource.Handle;

                s_captions ??= new();
                s_visualWindows ??= new();

                if (s_captions.ContainsKey(windowHandle))
                {
                    throw new InvalidOperationException(StringResources.CaptionIsAlreadySetToAnotherVisual);
                }

                bool hasHookBefore = HasWindowCaptionButton(windowHandle);

                s_captions[windowHandle] = visual;
                s_visualWindows[visual] = windowHandle;

                if (!hasHookBefore)
                {
                    hwndSource.AddHook(WindowHitTestInteropHook);
                }
            }
            else
            {
                if (s_captions is null ||
                    s_visualWindows is null)
                {
                    return;
                }

                if (hwndSource is null)
                {
                    if (!s_visualWindows.TryGetValue(visual, out var handle))
                    {
                        throw new InvalidOperationException();
                    }

                    hwndSource = HwndSource.FromHwnd(handle);
                }

                var windowHandle = hwndSource.Handle;

                s_captions.Remove(windowHandle);
                s_visualWindows.Remove(visual);

                if (s_captions.Count == 0)
                {
                    s_captions = null;
                }

                if (s_visualWindows.Count == 0)
                {
                    s_visualWindows = null;
                }

                if (!HasWindowCaptionButton(windowHandle))
                {
                    hwndSource.RemoveHook(WindowHitTestInteropHook);
                }
            }
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
                ApplyCorner(hwndSource, GetCorner(d));
        }

        private static void EventHandlerApplyCaptionColor(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyCaptionColor(hwndSource, GetCaptionColor(d));
        }

        private static void EventHandlerApplyTextColor(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyTextColor(hwndSource, GetTextColor(d));
        }

        private static void EventHandlerApplyBorderColor(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyBorderColor(hwndSource, GetBorderColor(d));
        }

        private static void EventHandlerApplyDarkMode(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyDarkMode(hwndSource, GetIsDarkMode(d));
        }

        private static void EventHandlerApplyAccent(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyAccent(sender as Window, hwndSource, GetAccentState(d), GetAccentGradientColor(d), GetAccentBorder(d));
        }

        private static void EventHandlerApplyIsCaptionVisible(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyIsCaptionVisible(hwndSource, GetIsCaptionVisible(d));
        }

        private static void EventHandlerApplyIsCaptionMenuVisible(object? sender, EventArgs e)
        {
            if (sender is DependencyObject d &&
                GetWindowHwndSource(d) is HwndSource hwndSource)
                ApplyIsCaptionMenuVisible(hwndSource, GetIsCaptionMenuVisible(d));
        }

        #endregion

        [StructLayout(LayoutKind.Explicit)]
        private struct XYInLParam
        {
            [FieldOffset(0)]
            public short X;

            [FieldOffset(2)]
            public short Y;
        }
    }
}
