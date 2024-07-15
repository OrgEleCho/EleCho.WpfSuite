using System;
using System.Windows;

namespace EleCho.WpfSuite.FluentDesign
{
    public class FluentThemeResources : ResourceDictionary
    {
        private readonly ResourceDictionary _lightThemeResources = new();
        private readonly ResourceDictionary _darkThemeResources = new();

        public FluentThemeResources()
        {
            _lightThemeResources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/Themes/Light.xaml") };
            _darkThemeResources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/EleCho.WpfSuite.FluentDesign;component/Themes/Dark.xaml") };

            MergedDictionaries.Add(_lightThemeResources);
        }

        public bool IsDarkMode
        {
            get => MergedDictionaries.Contains(_darkThemeResources);
            set
            {
                MergedDictionaries.Clear();

                if (value)
                {
                    MergedDictionaries.Add(_darkThemeResources);
                }
                else
                {
                    MergedDictionaries.Add(_lightThemeResources);
                }
            }
        }
    }
}
