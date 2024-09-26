using System.Windows;
using Microsoft.Win32;

namespace EleCho.WpfSuite.FluentDesign
{
    internal static class InternalUtilities
    {

        /// <summary>
        /// Get brightness of a color
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float GetBrightness(float r, float g, float b)
        {
            // 将 RGB 值转换为 YUV 值
            float y = 0.299f * r + 0.587f * g + 0.114f * b;

            // 计算亮度值
            return y;
        }

        private static bool IsDarkThemeDependOnColor()
        {
            var themeColor = SystemParameters.WindowGlassColor;
            var brightness = GetBrightness(themeColor.ScR, themeColor.ScG, themeColor.ScB);

            return brightness < 0.3;
        }

        private static bool? IsDarkThemeDependOnRegistry()
        {
            const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
            const string RegistryValueName = "AppsUseLightTheme";
            // 这里也可能是LocalMachine(HKEY_LOCAL_MACHINE)
            // see "https://www.addictivetips.com/windows-tips/how-to-enable-the-dark-theme-in-windows-10/"
            object? registryValueObject = Registry.CurrentUser.OpenSubKey(RegistryKeyPath)?.GetValue(RegistryValueName);
            if (registryValueObject is null)
                return null;
            return (int)registryValueObject > 0 ? false : true;
        }

        public static bool IsDarkTheme()
        {
            return IsDarkThemeDependOnRegistry() ?? IsDarkThemeDependOnColor();
        }
    }
}
