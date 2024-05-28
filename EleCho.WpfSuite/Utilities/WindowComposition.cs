using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;
using static EleCho.WpfSuite.WindowComposition.NativeDefinition;

namespace EleCho.WpfSuite
{
    public partial class WindowComposition
    {
        static readonly Version s_windows10_1809 = new Version(10, 0, 17763);
        static readonly Version s_windows10 = new Version(10, 0);
        static readonly Version s_windows11 = new Version(10, 0, 22621);

        static readonly Version s_currentWindows = Environment.OSVersion.Version;

        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }


        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetIsDarkMode(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDarkModeProperty);
        }

        public static void SetIsDarkMode(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDarkModeProperty, value);
        }


        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static Color GetGradientColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(GradientColorProperty);
        }

        public static void SetGradientColor(DependencyObject obj, Color value)
        {
            obj.SetValue(GradientColorProperty, value);
        }


        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetUseMica(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseMicaProperty);
        }

        public static void SetUseMica(DependencyObject obj, bool value)
        {
            obj.SetValue(UseMicaProperty, value);
        }


        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetUseAcrylic(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseAcrylicProperty);
        }

        public static void SetUseAcrylic(DependencyObject obj, bool value)
        {
            obj.SetValue(UseAcrylicProperty, value);
        }


        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetUseLeftBorder(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseLeftBorderProperty);
        }

        public static void SetUseLeftBorder(DependencyObject obj, bool value)
        {
            obj.SetValue(UseLeftBorderProperty, value);
        }


        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetUseTopBorder(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseTopBorderProperty);
        }

        public static void SetUseTopBorder(DependencyObject obj, bool value)
        {
            obj.SetValue(UseTopBorderProperty, value);
        }


        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetUseRightBorder(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseRightBorderProperty);
        }

        public static void SetUseRightBorder(DependencyObject obj, bool value)
        {
            obj.SetValue(UseRightBorderProperty, value);
        }


        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static bool GetUseBottomBorder(DependencyObject obj)
        {
            return (bool)obj.GetValue(UseBottomBorderProperty);
        }

        public static void SetUseBottomBorder(DependencyObject obj, bool value)
        {
            obj.SetValue(UseBottomBorderProperty, value);
        }






        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(WindowComposition), new PropertyMetadata(false, IsEnabledPropertyChangedCallback));

        public static readonly DependencyProperty IsDarkModeProperty =
            DependencyProperty.RegisterAttached("IsDarkMode", typeof(bool), typeof(WindowComposition), new PropertyMetadata(false, CompositionPropertyChangedCallback));

        public static readonly DependencyProperty GradientColorProperty =
            DependencyProperty.RegisterAttached("GradientColor", typeof(Color), typeof(WindowComposition), new PropertyMetadata(Color.FromScRgb(1, 1, 1, 1), CompositionPropertyChangedCallback));

        public static readonly DependencyProperty UseMicaProperty =
            DependencyProperty.RegisterAttached("UseMica", typeof(bool), typeof(WindowComposition), new PropertyMetadata(true, CompositionPropertyChangedCallback));

        public static readonly DependencyProperty UseAcrylicProperty =
            DependencyProperty.RegisterAttached("UseAcrylic", typeof(bool), typeof(WindowComposition), new PropertyMetadata(true, CompositionPropertyChangedCallback));

        public static readonly DependencyProperty UseLeftBorderProperty =
            DependencyProperty.RegisterAttached("UseLeftBorder", typeof(bool), typeof(WindowComposition), new PropertyMetadata(true, CompositionPropertyChangedCallback));

        public static readonly DependencyProperty UseTopBorderProperty =
            DependencyProperty.RegisterAttached("UseTopBorder", typeof(bool), typeof(WindowComposition), new PropertyMetadata(true, CompositionPropertyChangedCallback));

        public static readonly DependencyProperty UseRightBorderProperty =
            DependencyProperty.RegisterAttached("UseRightBorder", typeof(bool), typeof(WindowComposition), new PropertyMetadata(true, CompositionPropertyChangedCallback));

        public static readonly DependencyProperty UseBottomBorderProperty =
            DependencyProperty.RegisterAttached("UseBottomBorder", typeof(bool), typeof(WindowComposition), new PropertyMetadata(true, CompositionPropertyChangedCallback));


        private static int CreateColorInteger(Color color)
        {
            return
                color.R << 0 |
                color.G << 8 |
                color.B << 16 |
                color.A << 24;
        }

        private static void IsEnabledPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window)
                return;

            var windowSource = (HwndSource)PresentationSource.FromVisual(window);

            if (windowSource is not null)
            {
                Composite(window);
            }
            else if (e.NewValue is true)
            {
                CompositeAfterSourceInitialized(window);
            }
        }

        private static void CompositionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window)
                return;

            var windowSource = (HwndSource)PresentationSource.FromVisual(window);

            if (windowSource is not null)
            {
                Composite(window);
            }
        }

        private static unsafe void CompositeAfterSourceInitialized(Window window)
        {
            EventHandler? sourceInitialized = null;

            sourceInitialized = (s, e) =>
            {
                Composite(window);
                window.SourceInitialized -= sourceInitialized;
            };

            window.SourceInitialized += sourceInitialized;
        }

        private static unsafe void Composite(Window window)
        {
            var windowHelper = new WindowInteropHelper(window);
            var useMica = GetUseMica(window);
            var useAcrylic = GetUseAcrylic(window);
            var color = GetGradientColor(window);

            var canUseDwmApi = useMica || useAcrylic;
            var shouldUseCompositionApi = !useMica && color.A != 255;

            if (s_currentWindows >= s_windows11 && canUseDwmApi && !shouldUseCompositionApi)
            {
                const int BackdropTypeNone = 1;
                const int BackdropTypeMica = 2;
                const int BackdropTypeAcrylic = 3;

                var isDarkMode = GetIsDarkMode(window) ? 1 : 0;
                var backdropType = GetUseMica(window) ? BackdropTypeMica : BackdropTypeAcrylic;

                if (!GetIsEnabled(window))
                {
                    backdropType = BackdropTypeNone;
                }

                // set backdrop type
                DwmSetWindowAttribute(windowHelper.Handle, DwmWindowAttribute.USE_IMMERSIVE_DARK_MODE, (nint)(void*)&isDarkMode, (uint)sizeof(int));
                DwmSetWindowAttribute(windowHelper.Handle, DwmWindowAttribute.SYSTEMBACKDROP_TYPE, (nint)(void*)&backdropType, (uint)sizeof(int));

                // update window
                if (window.WindowStyle == WindowStyle.None)
                {
                    if (window.IsLoaded)
                    {
                        InvalidateRect(windowHelper.Handle, IntPtr.Zero, true);
                        UpdateWindow(windowHelper.Handle);
                    }
                    else
                    {
                        RoutedEventHandler? eventHandler = null;
                        eventHandler = (s, e) =>
                        {
                            InvalidateRect(windowHelper.Handle, IntPtr.Zero, true);
                            UpdateWindow(windowHelper.Handle);

                            window.Loaded -= eventHandler;
                        };

                        window.Loaded += eventHandler;
                    }
                }

                var chrome = WindowChrome.GetWindowChrome(window);
                if (chrome is null ||
                    chrome.GlassFrameThickness == default)
                {
                    throw new InvalidOperationException("Mica and Acrylic backdrop will not visible when WindowChrome.GlassFrameThickness is 0, you can set it to -1 to apply backdrop to the whole window");
                }
            }
            else
            {
                var accentPolicy = new AccentPolicy();
                var windowCompositionAttributeData = new WindowCompositionAttributeData();

                windowCompositionAttributeData.Attribute = WindowCompositionAttribute.WcaAccentPolicy;
                windowCompositionAttributeData.DataPointer = (nint)(void*)&accentPolicy;
                windowCompositionAttributeData.DataSize = (uint)sizeof(AccentPolicy);

                if (!GetIsEnabled(window))
                {
                    accentPolicy.AccentState = AccentState.Disabled;
                }
                else
                {
                    // check requirements
                    if (!window.AllowsTransparency)
                    {
                        throw new InvalidOperationException("'AllowTransparency' should be set to true for custom gradient color acrylic backdrop or blur backdrop");
                    }

                    // accents
                    if (GetUseAcrylic(window) && s_currentWindows >= s_windows10_1809)
                    {
                        accentPolicy.AccentState = AccentState.EnableAcrylicBlurBehind;
                        accentPolicy.GradientColor = CreateColorInteger(color);
                    }
                    else
                    {
                        accentPolicy.AccentState = AccentState.EnableBlurBehind;
                    }

                    // window border and drop shadow
                    if (GetUseLeftBorder(window))
                    {
                        accentPolicy.AccentFlags |= AccentFlags.LeftBorder;
                    }

                    if (GetUseTopBorder(window))
                    {
                        accentPolicy.AccentFlags |= AccentFlags.TopBorder;
                    }

                    if (GetUseRightBorder(window))
                    {
                        accentPolicy.AccentFlags |= AccentFlags.RightBorder;
                    }

                    if (GetUseBottomBorder(window))
                    {
                        accentPolicy.AccentFlags |= AccentFlags.BottomBorder;
                    }
                }


                SetWindowCompositionAttribute(windowHelper.Handle, ref windowCompositionAttributeData);
            }
        }
    }
}
