using System;
using System.Runtime.InteropServices;

namespace EleCho.WpfSuite
{
    public partial class WindowComposition
    {
        internal static class NativeDefinition
        {
            [DllImport("DWMAPI")]
            public static extern nint DwmSetWindowAttribute(nint hwnd, DwmWindowAttribute attribute, nint pointerValue, uint pointerSize);

            [DllImport("User32")]
            public static extern bool SetWindowCompositionAttribute(nint hwnd, ref WindowCompositionAttributeData data);

            [DllImport("User32")]
            public static extern bool GetWindowCompositionAttribute(nint hwnd, ref WindowCompositionAttributeData data);

            [DllImport("user32.dll")]
            public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

            [DllImport("User32")]
            public static extern bool UpdateWindow(nint hwnd);


            [StructLayout(LayoutKind.Sequential)]
            public struct WindowCompositionAttributeData
            {
                /// <summary>
                /// A flag describing which value to get or set, specified as a value of the <see cref="WindowCompositionAttribute"/> enumeration. 
                /// This parameter specifies which attribute to get or set, and the pvData member points to an object containing the attribute value.
                /// </summary>
                public WindowCompositionAttribute Attribute;

                /// <summary>
                /// When used with the GetWindowCompositionAttribute function, this member contains a pointer to a variable that will hold the value of the requested attribute when the function returns. <br/>
                /// When used with the SetWindowCompositionAttribute function, it points an object containing the attribute value to set. <br/>
                /// The type of the value set depends on the value of the Attrib member.
                /// </summary>
                public nint DataPointer;

                /// <summary>
                /// The size of the object pointed to by the pvData member, in bytes.
                /// </summary>
                public uint DataSize;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct AccentPolicy
            {
                public AccentState AccentState;
                public AccentFlags AccentFlags;
                public int GradientColor;
                public int AnimationId;
            }

            [Flags]
            public enum AccentFlags
            {
                None = 0,
                ExtendSize = 0x4,
                LeftBorder = 0x20,
                TopBorder = 0x40,
                RightBorder = 0x80,
                BottomBorder = 0x100
            }

            public enum WindowCompositionAttribute
            {
                // 省略其他未使用的字段
                WcaAccentPolicy = 19,
                // 省略其他未使用的字段
            }

            public enum AccentState
            {
                Disabled,
                EnableGradient = 1,
                EnableTransparent = 2,
                EnableBlurBehind = 3,
                EnableAcrylicBlurBehind = 4,
                EnableHostBackdrop = 5,
                InvalidState = 6,
            }

            public enum DwmWindowAttribute
            {
                NCRENDERING_ENABLED,
                NCRENDERING_POLICY,
                TRANSITIONS_FORCEDISABLED,
                ALLOW_NCPAINT,
                CAPTION_BUTTON_BOUNDS,
                NONCLIENT_RTL_LAYOUT,
                FORCE_ICONIC_REPRESENTATION,
                FLIP3D_POLICY,
                EXTENDED_FRAME_BOUNDS,
                HAS_ICONIC_BITMAP,
                DISALLOW_PEEK,
                EXCLUDED_FROM_PEEK,
                CLOAK,
                CLOAKED,
                FREEZE_REPRESENTATION,
                PASSIVE_UPDATE_MODE,
                USE_HOSTBACKDROPBRUSH,
                USE_IMMERSIVE_DARK_MODE = 20,
                WINDOW_CORNER_PREFERENCE = 33,
                BORDER_COLOR,
                CAPTION_COLOR,
                TEXT_COLOR,
                VISIBLE_FRAME_BORDER_THICKNESS,
                SYSTEMBACKDROP_TYPE,
                LAST
            }
        }
    }
}
