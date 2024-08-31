using System;
using System.Windows;
using Microsoft.Win32;

namespace EleCho.WpfSuite.FluentDesign
{
    public class FluentThemeResources : ResourceDictionary
    {
        private readonly ResourceDictionary _lightThemeResources = new();
        private readonly ResourceDictionary _darkThemeResources = new();
        private ApplicationTheme? _applicationTheme;

        public FluentThemeResources()
        {
            _lightThemeResources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/Themes/Light.xaml") };
            _darkThemeResources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/Themes/Dark.xaml") };

            Theme = ApplicationTheme.Auto;
        }

        public ApplicationTheme Theme
        {
            get
            {
                if (_applicationTheme is not null)
                {
                    return _applicationTheme.Value;
                }

                if (MergedDictionaries.Contains(_lightThemeResources))
                {
                    return ApplicationTheme.Light;
                }
                else if (MergedDictionaries.Contains(_darkThemeResources))
                {
                    return ApplicationTheme.Dark;
                }
                else
                {
                    return ApplicationTheme.Unknown;
                }
            }
            set
            {
                if (_applicationTheme == value)
                {
                    return;
                }

                if (_applicationTheme == ApplicationTheme.Auto)
                {
                    SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
                }

                MergedDictionaries.Clear();

                switch (value)
                {
                    case ApplicationTheme.Light:
                    {
                        MergedDictionaries.Add(_lightThemeResources);
                        break;
                    }
                    case ApplicationTheme.Dark:
                    {
                        MergedDictionaries.Add(_darkThemeResources);
                        break;
                    }
                    case ApplicationTheme.Auto:
                    {
                        ApplyAutoTheme();
                        SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
                        break;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                    }
                }

                _applicationTheme = value;
            }
        }

        public ApplicationTheme ActualTheme
        {
            get
            {
                if (MergedDictionaries.Contains(_lightThemeResources))
                {
                    return ApplicationTheme.Light;
                }
                else if (MergedDictionaries.Contains(_darkThemeResources))
                {
                    return ApplicationTheme.Dark;
                }
                else
                {
                    return ApplicationTheme.Unknown;
                }
            }
        }

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (_applicationTheme != ApplicationTheme.Auto)
            {
                return;
            }

            ApplyAutoTheme();
        }

        private void ApplyAutoTheme()
        {
            if (!InternalUtilities.IsDarkTheme())
            {
                MergedDictionaries.Add(_lightThemeResources);
            }
            else
            {
                MergedDictionaries.Add(_darkThemeResources);
            }
        }
    }
}