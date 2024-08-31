using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;

namespace EleCho.WpfSuite.FluentDesign
{
    public static class ApplicationThemeUtilities
    {
        private static bool _isWatchingSystemTheme = false;

        private static FluentResources? FindApplicationFluentResources()
        {
            Queue<ResourceDictionary> resourceDictionaries = new Queue<ResourceDictionary>();

            if (Application.Current.Resources is not null)
            {
                resourceDictionaries.Enqueue(Application.Current.Resources);
            }

            while (resourceDictionaries.Count > 0)
            {
                var currentResource = resourceDictionaries.Dequeue();

                if (currentResource is FluentResources fluentResources)
                {
                    return fluentResources;
                }

                foreach (var mergedDictionary in currentResource.MergedDictionaries)
                {
                    resourceDictionaries.Enqueue(mergedDictionary);
                }
            }

            return null;
        }

        private static void ApplyThemeForAllWindows(ApplicationTheme actualApplicationTheme)
        {
            foreach (Window window in Application.Current.Windows)
            {
                WindowOption.SetIsDarkMode(window, actualApplicationTheme == ApplicationTheme.Dark);
            }
        }

        public static ApplicationTheme GetApplicationTheme()
        {
            var applicationFluentResources = FindApplicationFluentResources();

            if (applicationFluentResources is not null)
            {
                return applicationFluentResources.ActualTheme;
            }

            int lightWindowCount = 0;
            int darkWindowCount = 0;

            foreach (Window window in Application.Current.Windows)
            {
                if (WindowOption.GetIsDarkMode(window))
                {
                    darkWindowCount++;
                }
                else
                {
                    lightWindowCount++;
                }
            }

            if (lightWindowCount >= darkWindowCount)
            {
                return ApplicationTheme.Light;
            }
            else
            {
                return ApplicationTheme.Dark;
            }
        }

        public static void SetApplicationTheme(ApplicationTheme theme)
        {
            var actualTheme = theme;
            var applicationFluentResources = FindApplicationFluentResources();

            if (theme != ApplicationTheme.Auto && _isWatchingSystemTheme)
            {
                SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
                _isWatchingSystemTheme = false;
            }

            if (theme == ApplicationTheme.Auto)
            {
                SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
                _isWatchingSystemTheme = true;
            }

            if (applicationFluentResources is not null)
            {
                applicationFluentResources.Theme = theme;
                actualTheme = applicationFluentResources.ActualTheme;
            }

            if (actualTheme == ApplicationTheme.Auto)
            {
                actualTheme = ApplicationTheme.Light;
            }

            ApplyThemeForAllWindows(actualTheme);
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (!_isWatchingSystemTheme)
            {
                SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
                return;
            }

            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                ApplyThemeForAllWindows(InternalUtilities.IsDarkTheme() ? ApplicationTheme.Dark : ApplicationTheme.Light);
            });
        }
    }
}
